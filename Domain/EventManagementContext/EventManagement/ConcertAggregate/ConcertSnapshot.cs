using Infrastructure.Persistence;
using Shared;
using Shared.Date;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    [DataContract]
    public class ConcertSnapshot : IRehydrateAggregate<Concert>
    {
        [DataMember]
        public Identity Id { get; set; }
        [DataMember]
        public DateTime ConcertDate { get; set; }
        [DataMember]
        public string Organizer { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TitleGeo { get; set; }
        [DataMember]
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

        public Concert ToAggregate()
        {
            return new Concert(new ConcertId(this.Id.Value),
                new EventTitleSummary(new GeoTitle(TitleGeo)).WithAnotherTitle(new EngTitle(TitleEng)),
                new EventDescription(ConcertDate, Description,
                new SystemDate(DateTime.Now)));
        }
    }
}
