using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.EventStore
{
    public interface IEventStore
    {
        void Store(Event evnt);
        IEnumerable<Event> ChangesFor(string aggregateRootId);
    }
}
