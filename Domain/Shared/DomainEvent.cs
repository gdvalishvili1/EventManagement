using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shared
{
    [DataContract]
    public abstract class DomainEvent
    {
        [DataMember]
        public string AggregateRootId { get; set; }
        [DataMember]
        public DateTime OccuredOn { get; set; }
        [DataMember]
        public string EventType { get; set; }
    }

    public abstract class VersionedDomainEvent : DomainEvent
    {
        [DataMember]
        public int Version { get; set; }
    }
}

