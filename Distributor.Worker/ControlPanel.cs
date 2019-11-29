using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Distributor.API.Contracts;
using Distributor.Messaging;
using Serilog;

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