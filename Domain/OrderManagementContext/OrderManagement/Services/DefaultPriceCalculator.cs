using System;
using System.Collections.Generic;
using System.Text;
using OrderManagement.Domain.OrderAggregate;
using Shared.Models.Money;
using System.Linq;

namespace OrderManagement.Domain.Services
{
    public class DefaultPriceCalculator : IPriceCalculator
    {
        public Money Total(IEnumerable<OrderItem> items)
        {
            //TODO: get seatType prices from order Management context read model
            return new Money(items.Sum(x => x.Quantity * 2));
        }
    }
}
