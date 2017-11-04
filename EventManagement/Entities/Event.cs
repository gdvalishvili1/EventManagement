using EventManagement.ValueObjects;
using Shared;

namespace EventManagement.Entities
{
    public abstract class Event : AggregateRoot<EventId>
    {
        protected EventDescription _eventDescription { get; set; }

        protected EventTitleSummary _eventTitle { get; set; }
        public Event()
        {

        }
        public Event(EventId eventId, EventDescription eventDescription, EventTitleSummary title)
        {
            _eventDescription = eventDescription;
            _eventTitle = title;
            Id = eventId;
        }
    }
}
