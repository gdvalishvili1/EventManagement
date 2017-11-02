using Infrastructure.EventStore;
using Shared;
using System.Collections.Generic;

namespace Infrastructure.EventSourcedAggregateRoot
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
            _root.Changes().ForEach(evnt => _eventStore.Store(new EventStore.Event(evnt)));
        }
    }
}
