using EventManagement.Domain.ConcertAggregate;
using EventManagement.Entities;
using EventManagement.ValueObjects;
using Shared.Date;
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
        public Concert Create(string titleGeo, string titleEng, string description, DateTime concertDate, ISystemDate systemDate = null)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate, null, systemDate);
        }

        public Concert Create(ConcertId id, string titleGeo, string titleEng, string description, DateTime concertDate, ISystemDate systemDate = null)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate, id, systemDate);
        }
        public Concert CreateFrom(ConcertSnapshot snapshot)
        {
            return new Concert(new ConcertId(snapshot.Id.ToString()),
                new EventTitleSummary(new GeoTitle(snapshot.TitleGeo)),
                new EventDescription(snapshot.ConcertDate, snapshot.Description, SystemDate.Now()),
                new EventOrganizer(snapshot.Organizer)
                );
        }
        private Concert CreateInternal(string titleGeo, string titleEng, string description, DateTime concertDate, ConcertId id = null, ISystemDate systemDate = null)
        {
            return new Concert(
                id ?? new ConcertId(Guid.NewGuid().ToString()),
                new EventTitleSummary(new GeoTitle(titleGeo)).WithAnotherTitle(new EngTitle(titleEng)),
                new EventDescription(concertDate, description, systemDate ?? SystemDate.Now())
                );
        }
    }
}
