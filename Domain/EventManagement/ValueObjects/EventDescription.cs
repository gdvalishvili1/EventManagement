using System;

namespace EventManagement.ValueObjects
{
    internal class EventDescription
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
        public DateTime EventDate { get; }
        public string Description { get; }

        public EventDescription ChangeDate(DateTime newDate)
        {
            return new EventDescription(newDate, Description);
        }
    }
}
