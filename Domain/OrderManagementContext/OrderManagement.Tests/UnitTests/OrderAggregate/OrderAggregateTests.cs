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
        private Order OrderWithItems()
        {
            var items = new List<OrderItem>
            {
                new OrderItem("",12),
                new OrderItem("",11)
            };
            var concertId = "123";
            var order = new Order(new OrderId(), concertId, "userid", items, new DefaultPriceCalculator());
            return order;
        }

        [TestMethod]
        public void CreatingOrder_WithProperValues_RaisesOrderPlacedEvent()
        {
            var order = this.OrderWithItems();

            Assert.IsTrue(RaiseSingleEventOf<OrderPlaced>(order), EventNotRaisedMessage<OrderPlaced>());
        }

        [TestMethod]
        public void CreatingOrder_WithProperValues_RaisesOrderTotalCalculatedevent()
        {
            var order = this.OrderWithItems();

            Assert.IsTrue(RaiseSingleEventOf<OrderTotalCalculated>(order), EventNotRaisedMessage<OrderTotalCalculated>());
        }
    }
}
