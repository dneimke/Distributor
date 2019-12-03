using System.Threading.Tasks;
using Distributor.Worker.Contracts;
using MassTransit;
using Serilog;

namespace Distributor.Worker.Faulty.Consumers
{
    public class FaultConsumer :
        IConsumer<Fault<DoWork>>
    {
        public Task Consume(ConsumeContext<Fault<DoWork>> context)
        {
            Log.Error("Failed to do {amount} of work with exception {exception}", context.Message.Message.Iterations, context.Message.Exceptions[0].Message);

            return Task.CompletedTask;
        }
    }
}