using System;
using Distributor.Messaging;

namespace Distributor.Worker.Contracts
{
    public class ValueCreated : IEvent
    {
        public ValueCreated(decimal amount, Guid workerId)
        {
            Amount = amount;
            WorkerId = workerId;
        }

        public decimal Amount { get; set; }
        public Guid WorkerId { get; set; }
    }
}
