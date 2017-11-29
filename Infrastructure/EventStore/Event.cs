using Shared;
using System;

namespace Infrastructure.EventStore
{
    public class Event
    {
        public Event(VersionedDomainEvent evnt)
        {
            AggregateRootId = evnt.Envilope.AggregateRootId;
            Date = evnt.Envilope.Date;
            EventName = evnt.Envilope.EventType;
            Data = evnt;
        }
        public string AggregateRootId { get; }
        public DateTime Date { get; }
        public string EventName { get; }
        public int Version { get; }
        public VersionedDomainEvent Data { get; }
    }
}
