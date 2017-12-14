using OrderManagement.Domain.OrderAggregate;
using OrderManagement.Domain.Services;
using Shared;
using Shared.Models.Money;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<OrderItem> Items { get; set; }
        private Order(string id, IEnumerable<VersionedDomainEvent> history)
        {
            Id = new OrderId(id);
            this.Load(history);
        }

        public Order(OrderId id, string concertId, IEnumerable<OrderItem> items, IPriceCalculator priceCalculator)
        {
            if (string.IsNullOrWhiteSpace(concertId))
            {
                throw new ArgumentException("concert id should not be null", nameof(concertId));
            }
            Id = id ?? throw new ArgumentNullException(nameof(id));
            this.ApplyChange(new OrderPlaced(concertId, items.ToList()));
            this.ApplyChange(new OrderTotalCalculated(priceCalculator.Total(items).ToValue()));
        }

        public OrderPlaced Place()
        {
            return new OrderPlaced(ConcertId, Items);
        }

        private void On(OrderPlaced orderPlaced)
        {
            ConcertId = orderPlaced.ConcertId;
            Items = OrderItem.From(orderPlaced.OrderItems);
        }

        private void On(OrderTotalCalculated orderTotalCalculated)
        {
            Total = Money.From(orderTotalCalculated.Total);
        }
    }
}
