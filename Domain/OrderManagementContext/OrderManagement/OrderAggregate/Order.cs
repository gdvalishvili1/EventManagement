using OrderManagement.Domain.OrderAggregate;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.OrderAggregate
{
    public class OrderId : Identity
    {
        public OrderId()
        {
        }

        public OrderId(string value) : base(value)
        {
        }
    }

    public class Order : EventSourcedAggregateRoot<OrderId>
    {
        public override string Identity => Id.Value;

        public string ConcertId { get; private set; }
        public OrderId Id { get; }

        private decimal Total { get; set; }

        public Order(OrderId id, string concertId)
        {
            if (string.IsNullOrWhiteSpace(concertId))
            {
                throw new ArgumentException("concert id should not be null", nameof(concertId));
            }

            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = concertId;

            this.ApplyChange(new OrderPlaced(concertId));
            this.ApplyChange(new OrderTotalCalculated(12));
        }

        private void On(OrderPlaced orderPlaced)
        {
            ConcertId = orderPlaced.ConcertId;
        }

        private void On(OrderTotalCalculated orderTotalCalculated)
        {
            Total = orderTotalCalculated.Total;
        }
    }
}
