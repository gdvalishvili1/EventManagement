using System;
using System.Collections.Generic;
using System.Text;

namespace Events.EventManagement
{
    public class ConcertNameChanged : DomainEvent
    {
        public ConcertNameChanged(string aggregateRootId, string newName)
            : base(aggregateRootId, DateTime.Now)
        {
            NewName = newName;
        }

        public string NewName { get; }
    }
}
