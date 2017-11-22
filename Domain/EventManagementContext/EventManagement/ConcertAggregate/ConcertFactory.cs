using EventManagement.Entities;
using EventManagement.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    public interface IAggregateFactory<TAggregate, Tsnapshot>
    {
        TAggregate CreateFrom(Tsnapshot snapshot);
    }
    public class ConcertFactory : IAggregateFactory<Concert, ConcertSnapshot>
    {
        public Concert Create(string titleGeo, string titleEng, string description, DateTime concertDate)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate);
        }

        public Concert Create(ConcertId id, string titleGeo, string titleEng, string description, DateTime concertDate)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate, id);
        }
        public Concert CreateFrom(ConcertSnapshot snapshot)
        {
            //correct this
            return new Concert(new ConcertId(snapshot.Id.ToString()),
                new EventTitleSummary(new GeoTitle(snapshot.TitleGeo)),
                new EventDescription(snapshot.Date, snapshot.Description)
                );
        }
        private Concert CreateInternal(string titleGeo, string titleEng, string description, DateTime concertDate, ConcertId id = null)
        {
            return new Concert(
                id ?? new ConcertId(Guid.NewGuid().ToString()),
                new EventTitleSummary(new GeoTitle(titleGeo)).WithAnotherTitle(new EngTitle(titleEng)),
                new EventDescription(concertDate, description)
                );
        }
    }
}
