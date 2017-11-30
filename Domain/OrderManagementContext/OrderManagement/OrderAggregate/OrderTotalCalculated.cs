using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.OrderAggregate
{
    public class OrderTotalCalculated : VersionedDomainEvent
    {
        public OrderTotalCalculated(Tuple<string, decimal> total)
        {
            Total = total;
        }

        public Tuple<string, decimal> Total { get; }
    }
}
