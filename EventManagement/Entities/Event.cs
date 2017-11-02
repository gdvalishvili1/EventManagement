using EventManagement.Interfaces;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Entities
{
    public abstract class Event : AggregateRoot<EventId>
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

        public Event(IEventDescription eventDescription)
        {
            _eventDescription = eventDescription;
        }
    }
}
