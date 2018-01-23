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
        private string UserId { get; set; }
        private Order(string id, IEnumerable<VersionedDomainEvent> history)
        {
            Id = new OrderId(id);
            this.Load(history);
        }

        public Order(OrderId id, string concertId, string userId, IEnumerable<OrderItem> items, IPriceCalculator priceCalculator)
        {
            if (string.IsNullOrWhiteSpace(concertId))
            {
                throw new ArgumentException("concert id should not be null", nameof(concertId));
            }
            Id = id ?? throw new ArgumentNullException(nameof(id));
            this.ApplyChange(new OrderPlaced(concertId, userId, items.ToList()));
            this.ApplyChange(new OrderTotalCalculated(priceCalculator.Total(items).ToValue()));
        }

        private void On(OrderPlaced orderPlaced)
        {
            ConcertId = orderPlaced.ConcertId;
            Items = OrderItem.From(orderPlaced.OrderItems);
            UserId = orderPlaced.UserId;
        }

        private void On(OrderTotalCalculated orderTotalCalculated)
        {
            Total = Money.From(orderTotalCalculated.Total);
        }

        private void On(UserChanged userChanged)
        {
            UserId = userChanged.UserId;
        }

        public IEnumerable<VersionedDomainEvent> ChangeUser(string userId)
        {
            yield return new UserChanged(userId);
        }
    }
}
