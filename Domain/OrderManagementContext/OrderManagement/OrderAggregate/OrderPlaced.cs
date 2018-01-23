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
        public OrderPlaced(string concertId, string userId, List<OrderItem> orderItems)
        {
            ConcertId = concertId;
            UserId = userId;
            OrderItems = orderItems.Select(x => Tuple.Create(x.SeatTypeId, x.Quantity)).ToList();
        }

        [JsonConstructor]
        private OrderPlaced(string concertId, string userId, List<Tuple<string, int>> orderItems)
        {
            ConcertId = concertId;
            OrderItems = orderItems;
            UserId = userId;
        }
        [DataMember]
        public string ConcertId { get; }
        [DataMember]
        public string UserId { get; }
        [DataMember]
        public List<Tuple<string, int>> OrderItems { get; }

    }
}
