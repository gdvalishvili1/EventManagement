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
}

