using System.Threading.Tasks;
using MassTransit;

namespace Distributor.Messaging
{
    public interface IEventPublisher<in TEvent> where TEvent : class, IEvent
    {
        Task PublishEvent(TEvent @event);
    }

    public class EventPublisher<TEvent> : IEventPublisher<TEvent> where TEvent : class, IEvent
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishEvent(TEvent @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}