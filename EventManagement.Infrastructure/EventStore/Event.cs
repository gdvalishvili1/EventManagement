using Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.EventStore
{
    public class Event
    {
        public Event(DomainEvent evnt)
        {
            AggregateRootId = evnt.EventMetadata.AggregateRootId;
            Date = evnt.EventMetadata.Date;
            EventName = evnt.EventMetadata.EventType;
            Data = evnt;
        }
        public string AggregateRootId { get; }
        public DateTime Date { get; }
        public string EventName { get; }
        public int Version { get; }
        public DomainEvent Data { get; }
    }
}
