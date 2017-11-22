using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EventStore
{
    public interface IEventStore
    {
        void Store(Event evnt);
        IEnumerable<Event> ChangesFor(string aggregateRootId);
        T AggregateById<T>(string id, List<Infrastructure.EventStore.Event> changes);
    }
}
