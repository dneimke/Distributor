using System;
using System.Threading.Tasks;
using MassTransit;

namespace Distributor.Messaging
{
    public interface ICommandSender<in TCommand> where TCommand : class, ICommand
    {
        Task SendCommand(TCommand command);
    }

    public class CommandSender<TCommand> : ICommandSender<TCommand> where TCommand : class, ICommand
    {
        private readonly Uri _address;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CommandSender(ISendEndpointProvider sendEndpointProvider)
        {
            _address = new Uri(new Uri("rabbitmq://localhost:5672"), Exchange.GetName<TCommand>());
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendCommand(TCommand command)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(_address);
            await endpoint.Send(command);
        }
    }
}