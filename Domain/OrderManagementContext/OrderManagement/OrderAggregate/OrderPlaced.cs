using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.OrderAggregate
{
    public class OrderPlaced : VersionedDomainEvent, ICreateEvent
    {
        public OrderPlaced(string concertId)
        {
            ConcertId = concertId;
        }

        public string ConcertId { get; }
    }
}
