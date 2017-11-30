using Infrastructure.EventDispatching;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EventStore
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
            _eventDispatcher.Dispatch(evnt.Payload);
        }

        public IEnumerable<Event> ChangesFor(string aggregateRootId)
        {
            return Events.Where(x => x.AggregateRootId == Guid.Parse(aggregateRootId));
        }

        //public static T AggregateById<T>(string id, List<Infrastructure.EventStore.Event> changes)
        //    where T : IEventSourcedAggregateRoot
        //{
        //    T root = (T)Activator.CreateInstance(typeof(T), true);
        //    root.Apply(changes.Select(x => x.Payload).ToList());

        //    return root;
        //}
    }
}
