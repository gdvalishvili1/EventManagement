using EventManagement.ValueObjects;
using Shared;

namespace EventManagement.Entities
{
    public abstract class Event : AggregateRoot<EventId>
    {
        protected EventDescription _eventDescription { get; set; }
        public Event()
        {

        }
        public Event(EventDescription eventDescription)
        {
            _eventDescription = eventDescription;
        }
    }
}
