using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Distributor.Messaging;
using Distributor.Worker.Contracts;
using Distributor.Worker.Faulty.Consumers;
using MassTransit;
using Serilog;
using Serilog.Context;

namespace Distributor.Worker.Faulty
{
    class Program
    { 
        private static IBusControl _busControl;

        static async Task Main(string[] args)
        {
            ConfigureLogging();
            using var context = LogContext.PushProperty("WorkerId", Worker.Id);

            var container = LetThereBeIOC();

            _busControl = container.Resolve<IBusControl>();

            await _busControl.StartAsync();

            try
            {
                container.Resolve<ControlPanel>().Run();
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

                    bus.SubscribeToCommand<DoWork, DoWorkConsumer, FaultConsumer>(host, context);
                }));
            });

            return builder.Build();
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information($"Hello World from {Worker.Id}!");
        }
    }
}
