using Domain.AggregateBase;
using Events.EventManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement
{
    public interface IEventDescription
    {
        IEventDescription Rename(string newName);
        IEventDescription ChangeDate(DateTime newDate);
        string Name { get; }
        string Description { get; }
        DateTime EventDate { get; }
    }
    public class EventDescription : IEventDescription
    {
        public EventDescription(string name, DateTime eventDate)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("concert name must not be null");
            }
            if (eventDate < DateTime.Now)
            {
                throw new Exception("concert date must be future date");
            }

            Name = name;
            EventDate = eventDate;
        }
        public string Name { get; private set; }
        public DateTime EventDate { get; private set; }
        public string Description { get; private set; }

        public IEventDescription Rename(string newName)
        {
            return new EventDescription(newName, EventDate);
        }
        public IEventDescription ChangeDate(DateTime newDate)
        {
            return new EventDescription(Name, newDate);
        }
    }

    public class EventId
    {
        public EventId(string id = null)
        {
            this.Value = id == null ? Guid.NewGuid() : Guid.Parse(id);
        }
        private Guid Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public abstract class EventBase : AggregateRoot<EventId>
    {
        protected class NullEventDescription : IEventDescription
        {
            public string Name { get; private set; }
            public DateTime EventDate { get; private set; }
            public string Description { get; private set; }

            public IEventDescription Rename(string newName)
            {
                return new NullEventDescription();
            }
            public IEventDescription ChangeDate(DateTime newDate)
            {
                return new NullEventDescription();
            }
        }

        protected IEventDescription _eventDescription { get; set; }

        public EventBase(IEventDescription eventDescription)
        {
            _eventDescription = eventDescription;
        }
    }
    public class Concert : EventBase
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
            Id = new EventId(evnt.EventMetadata.AggregateRootId);
            _eventDescription = new EventDescription(evnt.Name, evnt.ConcertDate);
        }

        private void On(ConcertNameChanged evnt)
        {
            Id = new EventId(evnt.EventMetadata.AggregateRootId);
            ChangeName(evnt.NewName);
        }

        private void On(OrganizerAssigned evnt)
        {
            _organizer = evnt.Organizer;
        }
    }
}
