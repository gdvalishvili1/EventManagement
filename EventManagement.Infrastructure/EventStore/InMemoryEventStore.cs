using EventManagement.Infrastructure.EventDispatching;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.Infrastructure.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        IEventDispatcher<DomainEvent> _eventDispatcher;

        public InMemoryEventStore(IEventDispatcher<DomainEvent> eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public static List<Event> Events = new List<Event>();
        public void Store(Event evnt)
        {
            Events.Add(evnt);
            _eventDispatcher.Dispatch(evnt.Data);
        }

        public IEnumerable<Event> ChangesFor(string aggregateRootId)
        {
            return Events.Where(x => x.AggregateRootId == aggregateRootId);
        }
    }
}
