using Infrastructure.EventStore;
using Shared;
using Shared.Json;
using System.Collections.Generic;

namespace Infrastructure.EventSourcedAggregateRoot
{
    public class StoreAggregateRootChanges : IStoreAggregateRootChanges
    {
        IEventSourcedAggregateRoot _root;
        IEventStore _eventStore;

        public StoreAggregateRootChanges(IEventSourcedAggregateRoot root, IEventStore eventStore)
        {
            _root = root;
            _eventStore = eventStore;

        }
        public IReadOnlyList<DomainEvent> Changes { get; private set; }

        public void StoreChanges()
        {
            //foreach (var uncommitedChange in _root.UncommittedChanges())
            //{
            //    _eventStore.Store(Event.Build(uncommitedChange));
            //}
        }
    }
}
