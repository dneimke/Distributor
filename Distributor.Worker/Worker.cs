using System;

namespace Distributor.Worker
{
    public static class Worker
    {
        public static Guid Id { get; set; } = Guid.NewGuid();
    }
}