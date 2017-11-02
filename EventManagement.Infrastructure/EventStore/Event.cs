using Shared;
using System;

namespace EventManagement.Infrastructure.EventStore
{
    public class Event
    {
        public Event(DomainEvent evnt)
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
        public DomainEvent Data { get; }
    }
}
