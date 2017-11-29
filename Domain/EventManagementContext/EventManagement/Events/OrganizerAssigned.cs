using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Events
{
    public class OrganizerAssigned : DomainEvent
    {
        public OrganizerAssigned(string organizer)
        {
            Organizer = organizer;
        }

        public string Organizer { get; }
    }
}
