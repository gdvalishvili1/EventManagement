using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    public class ConcertSnapshot
    {
        public Identity Id { get; set; }
        public DateTime ConcertDate { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; }
        public string TitleGeo { get; set; }
        public string TitleEng { get; set; }
        public ConcertSnapshot(Identity id, DateTime date, string organizer, string description, string titleGeo, string titleEng)
        {
            Id = id;
            ConcertDate = date;
            Description = description;
            Organizer = organizer;
            TitleGeo = titleGeo;
            TitleEng = titleEng;
        }

        public static ConcertSnapshot CreateFrom(Concert concert)
        {
            IProvideSnapshot<ConcertSnapshot> snapshotProvider = concert;
            var concertSnapshot = snapshotProvider.Snapshot();
            return concertSnapshot;
        }
    }
}
