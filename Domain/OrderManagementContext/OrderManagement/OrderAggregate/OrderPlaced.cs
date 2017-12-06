using Newtonsoft.Json;
using OrderManagement.Domain.OrderAggregate;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OrderManagement.OrderAggregate
{
    [DataContract]
    public class OrderPlaced : VersionedDomainEvent, ICreateEvent
    {
        public OrderPlaced(string concertId, List<OrderItem> orderItems)
        {
            ConcertId = concertId;
            OrderItems = orderItems.Select(x => Tuple.Create(x.SeatTypeId, x.Quantity)).ToList();
        }

        [JsonConstructor]
        private OrderPlaced(string concertId, List<Tuple<string, int>> orderItems)
        {
            ConcertId = concertId;
            OrderItems = orderItems;
        }
        [DataMember]
        public string ConcertId { get; }
        [DataMember]
        public List<Tuple<string, int>> OrderItems { get; }

    }
}
