using System;
using Autofac;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace Distributor.Messaging
{
    public static class MassTransitExtensions
    {
        public static void SubscribeToCommand<TCommand, TConsumer>(
            this IRabbitMqBusFactoryConfigurator bus, IRabbitMqHost host, IComponentContext context)
            where TConsumer : class, IConsumer<TCommand>
            where TCommand : class, ICommand
        {
            bus.ReceiveEndpoint(host,
                Exchange.GetName<TCommand>(),
                endpoint =>
                {
                    endpoint.ConfigureConsumer<TConsumer>(context);
                });
        }

        public static void SubscribeToCommand<TCommand, TConsumer, TFaultConsumer>(
            this IRabbitMqBusFactoryConfigurator bus, IRabbitMqHost host, IComponentContext context)
            where TConsumer : class, IConsumer<TCommand>
            where TFaultConsumer: class, IConsumer<Fault<TCommand>>
            where TCommand : class, ICommand
        {
            bus.ReceiveEndpoint(host,
                Exchange.GetName<TCommand>(),
                endpoint =>
                {
                    endpoint.UseMessageRetry(r => r.Immediate(3));
                    endpoint.ConfigureConsumer<TConsumer>(context);
                });

            bus.ReceiveEndpoint(host,
                Exchange.GetName<TCommand>() + "_Fault",
                endpoint =>
                {
                    endpoint.ConfigureConsumer<TFaultConsumer>(context);
                });
        }

        public static void SubscribeToTopic<TEvent, TConsumer>(
            this IRabbitMqBusFactoryConfigurator bus, IRabbitMqHost host, IComponentContext context)
            where TConsumer : class, IConsumer<TEvent>
            where TEvent : class, IEvent
        {
            bus.ReceiveEndpoint(host,
                Exchange.GetName<TEvent>() + "_" + Guid.NewGuid().ToString("N"), // TODO: If we want this to persist across restarts it needs to be persisted.
                endpoint => { endpoint.ConfigureConsumer<TConsumer>(context); });
        }
    }
}