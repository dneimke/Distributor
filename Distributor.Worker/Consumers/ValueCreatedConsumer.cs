using System.Threading.Tasks;
using Distributor.Worker.Contracts;
using MassTransit;
using Serilog;

namespace Distributor.Worker.Consumers
{
    public class ValueCreatedConsumer : IConsumer<ValueCreated>
    {
        public Task Consume(ConsumeContext<ValueCreated> context)
        {
            Log.Information(
                context.Message.WorkerId == Worker.Id
                    ? "I created {value} of value"
                    : "{value} of value was created by {workerId}", 
                context.Message.Amount,
                context.Message.WorkerId.ToString("N"));

            return Task.CompletedTask;
        }
    }
}