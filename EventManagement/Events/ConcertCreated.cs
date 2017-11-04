using Shared;
using System;

namespace EventManagement.Events
{
    public class ConcertCreated : DomainEvent
    {
        public ConcertCreated(string aggregateRootId, string name, DateTime concertDate, string description)
            : base(aggregateRootId, DateTime.Now)
        {
            Name = name;
            ConcertDate = concertDate;
            ConcertDescription = description;
        }

        public string Name { get; }
        public DateTime ConcertDate { get; set; }
        public string ConcertDescription { get; set; }
    }
}
