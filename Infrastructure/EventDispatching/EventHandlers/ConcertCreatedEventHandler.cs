using EventManagement.Domain.ConcertAggregate.Events;
using EventManagement.EventHandlers;

namespace Infrastructure.EventDispatching.EventDispatchers
{
    public class ConcertCreatedEventHandler : IHandle<ConcertCreated>
    {
        //
        public void Handle(ConcertCreated @event)
        {
            new ConcertCreatedProjector().Project(@event);
        }
    }
}
