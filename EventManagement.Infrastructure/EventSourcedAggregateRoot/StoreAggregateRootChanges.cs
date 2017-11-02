using EventManagement.Infrastructure.EventStore;
using Messages;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.EventSourcedAggregateRoot
{
    public class StoreAggregateRootChanges : IStoreAggregateRootChanges
    {
        IEventSourcedAggregaterRoot _root;
        IEventStore _eventStore;

        public StoreAggregateRootChanges(IEventSourcedAggregaterRoot root, IEventStore eventStore)
        {
            _root = root;
            _eventStore = eventStore;

        }
        public IReadOnlyList<DomainEvent> Changes { get; private set; }

        public void StoreChanges()
        {
            _root.Changes().ForEach(evnt => _eventStore.Store(new Event(evnt)));
        }
    }
}
