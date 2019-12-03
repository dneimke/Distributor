using System;
using System.Threading.Tasks;
using Distributor.Messaging;
using Distributor.Worker.Contracts;
using MassTransit;
using Serilog;

namespace Distributor.Worker.Faulty.Consumers
{
    public class DoWorkConsumer : IConsumer<DoWork>
    {
        public Task Consume(ConsumeContext<DoWork> context)
        {
            Log.Information("{amount} of work was requested", context.Message.Iterations);

            throw new InvalidOperationException("I'm sorry Dave, I'm afraid I just can't do that");
        }
    }
}