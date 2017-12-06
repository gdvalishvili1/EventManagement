using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement.Domain.OrderAggregate;
using OrderManagement.Domain.Services;
using OrderManagement.OrderAggregate;
using Shared;
using Shared.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagement.Tests.UnitTests.OrderAggregate
{
    [TestClass]
    public class OrderAggregateTests : DomainTest
    {

        private Order OrderAggregate(List<OrderItem> items)
        {
            var concertId = "123";
            var order = new Order(new OrderId(), concertId, items, new DefaultPriceCalculator());
            return order;
        }

        [TestMethod]
        public void OrderCreated()
        {
            var items = new List<OrderItem>
            {
                new OrderItem("",12),
                new OrderItem("",11)
            };
            var order = this.OrderAggregate(items);

            Assert.IsTrue(RaiseSingleEventOf<OrderPlaced>(order), EventNotRaisedMessage<OrderPlaced>());
            Assert.IsTrue(RaiseSingleEventOf<OrderTotalCalculated>(order), EventNotRaisedMessage<OrderTotalCalculated>());
        }
    }
}
