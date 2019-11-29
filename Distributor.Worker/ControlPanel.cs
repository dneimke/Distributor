using System;

namespace Distributor.Worker
{
    public class ControlPanel
    {
        public void Run()
        {
            do
            {
                Console.WriteLine("Enter quit to exit");
                Console.Write("> ");
                var value = Console.ReadLine();

                if (value == "quit") break;
            }
            while (true);
        }
    }
}