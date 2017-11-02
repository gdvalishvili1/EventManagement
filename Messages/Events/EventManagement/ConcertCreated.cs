using Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events.EventManagement
{
    public class ConcertCreated : DomainEvent
    {
        public ConcertCreated(string aggregateRootId, string name, DateTime concertDate)
            : base(aggregateRootId, DateTime.Now)
        {
            Name = name;
            ConcertDate = concertDate;
        }

        public string Name { get; }
        public DateTime ConcertDate { get; set; }
    }
}
