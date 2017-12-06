using Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace OrderManagement.Domain.OrderAggregate
{
    [DataContract]
    public class OrderTotalCalculated : VersionedDomainEvent
    {
        public OrderTotalCalculated(Tuple<string, decimal> total)
        {
            Total = total;
        }
        [DataMember]
        public Tuple<string, decimal> Total { get; }
    }
}
