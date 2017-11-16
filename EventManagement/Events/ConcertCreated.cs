using Shared;
using System;

namespace EventManagement.Events
{
    public class ConcertCreated : DomainEvent
    {
        public ConcertCreated(string aggregateRootId, string titleGe, string titleEng,
            DateTime concertDate, string description)
            : base(aggregateRootId, DateTime.Now)
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
