using System;

namespace Distributor.Worker.Faulty
{
    public static class Worker
    {
        public static Guid Id { get; set; } = Guid.NewGuid();
    }
}