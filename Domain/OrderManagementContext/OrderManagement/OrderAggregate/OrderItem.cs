using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagement.Domain.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem(string seatTypeId, int quantity)
        {
            this.SeatTypeId = seatTypeId;
            this.Quantity = quantity;
        }

        public string SeatTypeId { get; private set; }

        public int Quantity { get; private set; }

        public static List<OrderItem> From(IEnumerable<Tuple<string, int>> tupleItems)
        {
            return tupleItems.Select(x => new OrderItem(x.Item1, x.Item2)).ToList();
        }
    }
}
