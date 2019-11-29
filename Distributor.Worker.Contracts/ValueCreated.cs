using System;
using Distributor.Messaging;

namespace Distributor.Worker.Contracts
{
    public class ValueCreated : IEvent
    {
        public ValueCreated(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; set; }
    }
}
