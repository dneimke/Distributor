using Distributor.Messaging;

namespace Distributor.API.Contracts
{
    public class DoWork : ICommand
    {
        public DoWork(string description, int iterations)
        {
            Description = description;
            Iterations = iterations;
        }

        public string Description { get; set; }
        public int Iterations { get; set; }
    }
}
