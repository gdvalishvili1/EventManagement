using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class DomainEvent
    {
        public string AggregateRootId { get; set; }
        public DateTime DateOccuredOn { get; set; }
        public string EventType { get; set; }
    }

    public abstract class VersionedDomainEvent : DomainEvent
    {
        public int Version { get; set; }
    }
}

