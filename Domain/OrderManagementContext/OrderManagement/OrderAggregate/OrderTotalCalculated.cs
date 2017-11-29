using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.OrderAggregate
{
    public class OrderTotalCalculated : VersionedDomainEvent
    {
        public OrderTotalCalculated(decimal total)
        {
            Total = total;
        }

        public decimal Total { get;  }
    }
}
