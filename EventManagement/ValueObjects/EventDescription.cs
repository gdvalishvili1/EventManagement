using System;

namespace EventManagement.ValueObjects
{
    public class EventDescription
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

        public EventDescription Rename(string newName)
        {
            return new EventDescription(newName, EventDate);
        }
        public EventDescription ChangeDate(DateTime newDate)
        {
            return new EventDescription(Name, newDate);
        }
    }
}
