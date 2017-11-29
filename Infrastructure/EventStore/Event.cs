using Shared;
using System;

namespace Infrastructure.EventStore
{
    public class Event
    {
        public Event(VersionedDomainEvent evnt)
        {
            AggregateRootId = evnt.AggregateRootId;
            Date = evnt.DateOccuredOn;
            EventName = evnt.EventType;
            Data = evnt;
        }
        public string AggregateRootId { get; }
        public DateTime Date { get; }
        public string EventName { get; }
        public int Version { get; }
        public VersionedDomainEvent Data { get; }
    }
}
