using EventManagement.Events;
using EventManagement.ValueObjects;
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
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; }
        public string TitleGeo { get; set; }
        public string TitleEng { get; set; }
        public ConcertSnapshot(DateTime date, string organizer, string description, string titleGeo, string titleEng)
        {
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

    public class Concert : Event,
        IProvideEntitySnapshot<ConcertSnapshot>
    {
        private string _organizer;

        private Concert()
            : base() { }

        public Concert(EventId id,
            EventTitleSummary title,
            EventDescription eventDescription)
            : base(id, eventDescription, title)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (title == null)
                throw new ArgumentNullException(nameof(title));

            if (eventDescription == null)
                throw new ArgumentNullException(nameof(eventDescription));

        }

        public static Concert CreateFrom(ConcertSnapshot snapshot)
        {
            return new Concert(new EventId(snapshot.Id),
                new EventTitleSummary(new GeoTitle(snapshot.TitleGeo)),
                new EventDescription(snapshot.Date, snapshot.Description)
                );
        }

        public void AssignOrganizer(string organizer)
        {
            if (string.IsNullOrEmpty(organizer))
                throw new ArgumentException(nameof(organizer));

            _organizer = organizer;
        }

        public void ChangeConcertTitle(EventTitleSummary newTitle)
        {
            _eventTitle = newTitle;
        }

        ConcertSnapshot IProvideEntitySnapshot<ConcertSnapshot>.Snapshot()
        {
            return new ConcertSnapshot(_eventDescription.EventDate, _organizer,
                _eventDescription.Description, _eventTitle.GeoTitle(), _eventTitle.EngTitle());
        }
    }
}
