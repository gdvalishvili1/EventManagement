using Shared;
using System;
using System.Runtime.Serialization;

namespace EventManagement.Domain.ConcertAggregate.Events
{
    [DataContract]
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

        [DataMember]
        public string TitleGe { get; }
        [DataMember]
        public string TitleEng { get; }
        [DataMember]
        public DateTime ConcertDate { get; }
        [DataMember]
        public string ConcertDescription { get; }
    }

    public class ConcertCreatedV2 : DomainEvent, ICreateEvent
    {
        public ConcertCreatedV2(string titleGe, string titleEng,
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
