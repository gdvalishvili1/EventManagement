using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events.EventManagement
{
    public class OrganizerAssigned : DomainEvent
    {
        public OrganizerAssigned(string aggregateRootId, string organizer)
            : base(aggregateRootId, DateTime.Now)
        {
            Organizer = organizer;
        }

        public string Organizer { get; }
    }
}
