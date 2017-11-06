using EventManagement.ValueObjects;
using Shared;

namespace EventManagement.Entities
{
    public abstract class Event : AggregateRoot<EventId>
    {
        protected EventDescription EventDescription { get; set; }

        protected EventTitleSummary EventTitle { get; set; }
        public Event()
        {

        }
        public Event(EventId eventId, EventDescription eventDescription, EventTitleSummary title)
        {
            EventDescription = eventDescription;
            EventTitle = title;
            Id = eventId;
        }
    }
}
