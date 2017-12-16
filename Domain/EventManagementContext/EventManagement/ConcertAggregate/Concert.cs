using EventManagement.Domain.ConcertAggregate;
using EventManagement.Events;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Newtonsoft.Json;
using Shared;
using System;

namespace EventManagement.ConcertAggregate
{
    public class Concert : AggregateRoot, IProvideSnapshot<ConcertSnapshot>
    {
        private EventDescription EventDescription { get; set; }
        private EventTitleSummary EventTitle { get; set; }
        private EventOrganizer Organizer { get; set; }
        public ConcertId Id { get; }
        public override string Identity => Id.Value;

        private Concert() : base() { }

        internal Concert(ConcertId id,
            EventTitleSummary eventTitle,
            EventDescription eventDescription)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            EventDescription = eventDescription ?? throw new ArgumentNullException(nameof(eventDescription));
            EventTitle = eventTitle ?? throw new ArgumentNullException(nameof(eventTitle));

            this.Apply(new ConcertCreated(eventTitle.GeoTitle(), eventTitle.EngTitle(), EventDescription.EventDate, EventDescription.Description));
        }

        [JsonConstructor]
        internal Concert(ConcertId id,
            EventTitleSummary eventTitle,
            EventDescription eventDescription,
            EventOrganizer organizer)
        {
            Organizer = organizer;

            Id = id ?? throw new ArgumentNullException(nameof(id));
            EventDescription = eventDescription ?? throw new ArgumentNullException(nameof(eventDescription));
            EventTitle = eventTitle ?? throw new ArgumentNullException(nameof(eventTitle));
        }

        public void AssignOrganizer(EventOrganizer organizer)
        {
            Organizer = organizer ?? throw new ArgumentNullException(nameof(organizer));

            this.Apply(new OrganizerAssigned(organizer));
        }

        public void ChangeConcertTitle(string newGeoTitle, string newEngTitle)
        {
            EventTitle = new EventTitleSummary(new GeoTitle(newGeoTitle))
                .WithAnotherTitle(new EngTitle(newEngTitle));
        }

        public void Postpone(DateTime date)
        {
            EventDescription = EventDescription.ChangeDate(date);
        }

        public void Archieve()
        {
            //change publish status and etc...
        }

        ConcertSnapshot IProvideSnapshot<ConcertSnapshot>.Snapshot()
        {
            return new ConcertSnapshot(Id, EventDescription.EventDate, Organizer?.Name,
                EventDescription.Description, EventTitle.GeoTitle(), EventTitle.EngTitle());
        }
    }
}
