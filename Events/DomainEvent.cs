using System;
using System.Collections.Generic;
using System.Text;

namespace Events
{
    public abstract class DomainEvent
    {
        public DomainEvent(string aggregateRootId, DateTime date)
        {
            EventMetadata = new Envilope(aggregateRootId, date);
        }
        public Envilope EventMetadata { get; }
    }
}

