using OrderManagement.Domain.OrderAggregate;
using Shared;
using Shared.Models.Money;
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
        private Money Total { get; set; }
        private Order(string id, IEnumerable<VersionedDomainEvent> history)
        {
            Id = new OrderId(id);
            this.Load(history);
        }

        public Order(OrderId id, string concertId)
        {
            if (string.IsNullOrWhiteSpace(concertId))
            {
                throw new ArgumentException("concert id should not be null", nameof(concertId));
            }

            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = concertId;

            this.ApplyChange(new OrderPlaced(concertId));
            this.ApplyChange(new OrderTotalCalculated(new Money("GEL", 12).ToValue()));
        }

        private void On(OrderPlaced orderPlaced)
        {
            ConcertId = orderPlaced.ConcertId;
        }

        private void On(OrderTotalCalculated orderTotalCalculated)
        {
            Total = Money.From(orderTotalCalculated.Total);
        }
    }
}
