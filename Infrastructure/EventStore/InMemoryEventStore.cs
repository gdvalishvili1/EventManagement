using Infrastructure.EventDispatching;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EventStore
{
    public class InMemoryEventStore
    {
        IEventDispatcher<DomainEvent> _eventDispatcher;
        public static List<Event> Events = new List<Event>();

        public InMemoryEventStore(IEventDispatcher<DomainEvent> eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public void Store(Event evnt)
        {
            Events.Add(evnt);
            _eventDispatcher.Dispatch(evnt.Payload);
        }

        public IEnumerable<Event> ChangesFor(string aggregateRootId)
        {
            return Events.Where(x => x.AggregateRootId == Guid.Parse(aggregateRootId));
        }
    }
}
