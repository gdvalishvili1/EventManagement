using Shared.Date;
using System;

namespace EventManagement.ValueObjects
{
    internal class EventDescription
    {
        private readonly ISystemDate _systemDate;
        public EventDescription(DateTime eventDate, string description, ISystemDate systemDate)
        {
            if (eventDate < systemDate.Today)
            {
                throw new Exception("concert date must be future date");
            }

            EventDate = eventDate;
            Description = description;
            _systemDate = systemDate;
        }
        public DateTime EventDate { get; }
        public string Description { get; }

        public EventDescription ChangeDate(DateTime newDate)
        {
            return new EventDescription(newDate, Description, _systemDate);
        }
    }
}
