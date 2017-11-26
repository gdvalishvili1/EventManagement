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

        public Order(string concertId)
        {

        }
    }
}
