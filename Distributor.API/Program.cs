using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Distributor.API.Consumers;
using Distributor.API.Contracts;
using Distributor.Messaging;
using Distributor.Worker.Contracts;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Serilog;

namespace Distributor.API
{
    class Program
    { 
        private static IBusControl _busControl;

        static async Task Main(string[] args)
        {
            ConfigureLogging();

            var container = LetThereBeIOC();

            _busControl = container.Resolve<IBusControl>();

            await _busControl.StartAsync();

            try
            {
                await container.Resolve<ControlPanel>().Run();
            }
            finally
            {
                await _busControl.StopAsync();
            }
        }

        private static IContainer LetThereBeIOC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ControlPanel>().SingleInstance();

            builder.AddMassTransit(x =>
            {
                // Registers the available consumers
                x.AddConsumers(Assembly.GetExecutingAssembly());

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(bus =>
                {
                    bus.RunSharedConfiguration();

                    var host = bus.Host(new Uri("rabbitmq://localhost:5672"), hostConfig =>
                    {
                        hostConfig.PublisherConfirmation = true;
                    });

                    // Wires up the registered consumers to incoming messages (TODO: this can be further abstracted to a convention based enumeration)
                    bus.SubscribeToTopic<ValueCreated, ValueCreatedConsumer>(host, context);
                }));
            });

            builder.RegisterGeneric(typeof(CommandSender<>)).As(typeof(ICommandSender<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(EventPublisher<>)).As(typeof(IEventPublisher<>)).InstancePerDependency();

            return builder.Build();
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Hello World!");
        }
    }

    
}
