using EventManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
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
}
