using System;
using Autofac;
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
                endpoint => { endpoint.ConfigureConsumer<TConsumer>(context); });
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