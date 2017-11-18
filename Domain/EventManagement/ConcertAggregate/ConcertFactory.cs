using EventManagement.Entities;
using EventManagement.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    public class ConcertFactory
    {
        public static Concert Create(string titleGeo, string titleEng, string description, DateTime concertDate)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate);
        }

        public static Concert Create(ConcertId id, string titleGeo, string titleEng, string description, DateTime concertDate)
        {
            return CreateInternal(titleGeo, titleEng, description, concertDate, id);
        }

        private static Concert CreateInternal(string titleGeo, string titleEng, string description, DateTime concertDate, ConcertId id = null)
        {
            return new Concert(
                id ?? new ConcertId(Guid.NewGuid().ToString()),
                new EventTitleSummary(new GeoTitle(titleGeo)).WithAnotherTitle(new EngTitle(titleEng)),
                new EventDescription(concertDate, description)
                );
        }
    }
}
