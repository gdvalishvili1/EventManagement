using System;

namespace EventManagement.ValueObjects
{
    public class EventDescription
    {
        public EventDescription(DateTime eventDate, string description)
        {
            if (eventDate < DateTime.Now)
            {
                throw new Exception("concert date must be future date");
            }

            EventDate = eventDate;
            Description = description;
        }
        public DateTime EventDate { get; private set; }
        public string Description { get; private set; }

        public EventDescription ChangeDate(DateTime newDate)
        {
            return new EventDescription(newDate, Description);
        }
    }
}
