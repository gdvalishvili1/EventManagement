using Shared;
using System;

namespace EventManagement.Domain.ConcertAggregate.Events
{
    public class ConcertCreated : DomainEvent, ICreateEvent
    {
        public ConcertCreated(string titleGe, string titleEng,
            DateTime concertDate, string description)
        {
            TitleGe = titleGe;
            TitleEng = titleEng;
            ConcertDate = concertDate;
            ConcertDescription = description;
        }

        public string TitleGe { get; }
        public string TitleEng { get; }
        public DateTime ConcertDate { get; }
        public string ConcertDescription { get; }
    }
}
