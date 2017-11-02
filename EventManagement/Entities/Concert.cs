using EventManagement.Events;
using EventManagement.Interfaces;
using EventManagement.ValueObjects;

namespace EventManagement.Entities
{
    public class Concert : Event
    {
        private string _organizer;
        private Concert() : base(new NullEventDescription())
        {

        }
        public Concert(IEventDescription eventDescription) : base(eventDescription)
        {
            Id = new EventId();
            _eventDescription = eventDescription;
            Emit(new ConcertCreated(Id.ToString(), eventDescription.Name, eventDescription.EventDate));
        }

        public void AssignOrganizer(string organizer)
        {
            _organizer = organizer;
            Emit(new OrganizerAssigned(Id.ToString(), organizer));
        }

        public void ChangeConcertName(string newName)
        {
            ChangeName(newName);
            Emit(new ConcertNameChanged(Id.ToString(), newName));
        }

        private void ChangeName(string newName)
        {
            _eventDescription = new EventDescription(newName, _eventDescription.EventDate);
        }

        private void On(ConcertCreated evnt)
        {
            Id = new EventId(evnt.Envilope.AggregateRootId);
            _eventDescription = new EventDescription(evnt.Name, evnt.ConcertDate);
        }

        private void On(ConcertNameChanged evnt)
        {
            Id = new EventId(evnt.Envilope.AggregateRootId);
            ChangeName(evnt.NewName);
        }

        private void On(OrganizerAssigned evnt)
        {
            _organizer = evnt.Organizer;
        }
    }
}
