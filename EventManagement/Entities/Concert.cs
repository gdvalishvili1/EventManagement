using EventManagement.Events;
using EventManagement.ValueObjects;
using System;

namespace EventManagement.Entities
{
    public class Concert : Event
    {
        private string _organizer;

        private Concert() : base() { }

        public Concert(EventId id, EventDescription eventDescription) : base(eventDescription)
        {
            ApplyChange(new ConcertCreated(id.ToString(), eventDescription.Name, eventDescription.EventDate));
        }

        public void AssignOrganizer(string organizer)
        {
            if (string.IsNullOrEmpty(organizer))
                throw new Exception("organizer name should not be null or empty");

            ApplyChange(new OrganizerAssigned(Id.ToString(), organizer));
        }

        public void ChangeConcertName(string newName)
        {
            ApplyChange(new ConcertNameChanged(Id.ToString(), newName));
        }

        private void On(ConcertCreated evnt)
        {
            Id = new EventId(evnt.Envilope.AggregateRootId);
            _eventDescription = new EventDescription(evnt.Name, evnt.ConcertDate);
        }

        private void On(ConcertNameChanged evnt)
        {
            Id = new EventId(evnt.Envilope.AggregateRootId);
            _eventDescription = new EventDescription(evnt.NewName, _eventDescription.EventDate);
        }

        private void On(OrganizerAssigned evnt)
        {
            Id = new EventId(evnt.Envilope.AggregateRootId);
            _organizer = evnt.Organizer;
        }
    }
}
