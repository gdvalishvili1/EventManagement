using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class DomainEvent
    {
        public DomainEvent(string aggregateRootId, DateTime date)
        {
            Envilope = new Envilope(aggregateRootId, date);
        }
        public Envilope Envilope { get; }
    }

    public abstract class VersionedDomainEvent : DomainEvent
    {
        public VersionedDomainEvent(string aggregateRootId, DateTime date, int aggrergateVersion) : base(aggregateRootId, date)
        {
            this.Version = aggrergateVersion + 1;
        }
        public int Version { get; set; }
    }
}

