using MassTransit;

namespace Distributor.Messaging
{
    public static class BusFactoryConfiguratorExtensions
    {
        public static void RunSharedConfiguration(this IBusFactoryConfigurator bus)
        {
            bus.UseSerilog();
        }
    }
}