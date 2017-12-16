using EventManagement.Domain.ConcertAggregate;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Events
{
    public class OrganizerAssigned : DomainEvent
    {
        public OrganizerAssigned(EventOrganizer organizer)
        {
            Organizer = organizer.Name;
        }

        public string Organizer { get; }
    }
}
