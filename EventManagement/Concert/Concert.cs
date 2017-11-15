using EventManagement.Events;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Newtonsoft.Json;
using Shared;
using System;

namespace EventManagement.Entities
{
    public interface IProvideEntitySnapshot<TSnapshot>
    {
        TSnapshot Snapshot();
    }
    public class ConcertSnapshot
    {
        public Identity Id { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; }
        public string TitleGeo { get; set; }
        public string TitleEng { get; set; }
        public ConcertSnapshot(Identity id, DateTime date, string organizer, string description, string titleGeo, string titleEng)
        {
            Id = id;
            Date = date;
            Description = description;
            Organizer = organizer;
            TitleGeo = titleGeo;
            TitleEng = titleEng;
        }

        public static ConcertSnapshot CreateFrom(Concert concert)
        {
            IProvideEntitySnapshot<ConcertSnapshot> snapshotProvider = concert;
            var concertSnapshot = snapshotProvider.Snapshot();
            return concertSnapshot;
        }
    }

    public class Concert : AggregateRoot, IProvideEntitySnapshot<ConcertSnapshot>
    {
        private EventDescription EventDescription { get; set; }
        private EventTitleSummary EventTitle { get; set; }
        private string Organizer { get; set; }
        private EventSeatSummary EventSeatSummary { get; set; }
        public EventId Id { get; }

        public override string Identity => Id.Value;

        private Concert() : base()
        {

        }

        internal Concert(EventId id,
            EventTitleSummary eventTitle,
            EventDescription eventDescription)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            EventDescription = eventDescription ?? throw new ArgumentNullException(nameof(eventDescription));
            EventTitle = eventTitle ?? throw new ArgumentNullException(nameof(eventTitle));
        }

        [JsonConstructor]
        private Concert(EventId id,
            EventTitleSummary eventTitle,
            EventDescription eventDescription,
            string organizer,
            EventSeatSummary eventSeatSummary) :
            this(id, eventTitle, eventDescription)
        {
            Organizer = organizer;
            EventSeatSummary = eventSeatSummary;
        }

        public static Concert CreateFrom(ConcertSnapshot snapshot)
        {
            //correct this
            return new Concert(new EventId(snapshot.Id.ToString()),
                new EventTitleSummary(new GeoTitle(snapshot.TitleGeo)),
                new EventDescription(snapshot.Date, snapshot.Description)
                );
        }

        public void AssignOrganizer(string organizer)
        {
            if (string.IsNullOrEmpty(organizer))
            {
                throw new ArgumentException(nameof(organizer));
            }

            Organizer = organizer;
        }

        public void ChangeConcertTitle(string newGeoTitle, string newEngTitle)
        {
            EventTitle = new EventTitleSummary(new GeoTitle(newGeoTitle)).WithAnotherTitle(new EngTitle(newEngTitle));
        }

        public void Postpone(DateTime date)
        {
            EventDescription.ChangeDate(date);
        }

        public void Archieve()
        {
            //change publish status and etc...
        }

        public void AddEventSeatSummary(EventSeatSummary eventSeatSummaryId)
        {
            EventSeatSummary = eventSeatSummaryId ?? throw new ArgumentNullException(nameof(eventSeatSummaryId));
        }

        ConcertSnapshot IProvideEntitySnapshot<ConcertSnapshot>.Snapshot()
        {
            return new ConcertSnapshot(Id, EventDescription.EventDate, Organizer, EventDescription.Description, EventTitle.GeoTitle(), EventTitle.EngTitle());
        }
    }
}
