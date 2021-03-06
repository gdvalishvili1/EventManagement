﻿using EventManagement.Application.UseCases.CreateNewConcert;
using EventManagement.Application.UseCases.CreateSeatType;
using EventManagement.ConcertAggregate;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Persistence;
using EventManagement.SeatTypeAggregate;
using Infrastructure.EventStore;
using Infrastructure.Persistence;
using OrderManagement.Domain.OrderAggregate;
using OrderManagement.Domain.Services;
using OrderManagement.OrderAggregate;
using Shared.Json;
using Shared.Models.Money;
using System;
using System.Collections.Generic;

namespace ConsoleTesting
{

    class Program
    {
        static void Main(string[] args)
        {
            //var items = new List<OrderItem>
            //{
            //    new OrderItem("",12),
            //    new OrderItem("",11)
            //};

            //var order = new Order(new OrderId(), "123", "userid", items, new DefaultPriceCalculator());

            //var ordersSql = new SqlEventSourcedRepository<Order>();
            //var ordersEventStore = new SqlEventSourcedRepository<Order>();

            //ordersSql.Store(order);

            //var order1 = ordersSql.Load("FDEC6890-4EAA-440B-9A8F-A37FD57366FA");
            //order1.ChangeUser("joni");
            //ordersSql.Store(order1);
            //var order2 = ordersSql.Load("");

            IConcertRepository concerts = new JsonConcertRepository(
               new JsonParser<ConcertSnapshot>(),
               new StorageOptions("ConcertsJson")
               );

            var createConcertCommandResult = new CreateNewConcertCommand(concerts,
                "Geo Title",
                "Eng Title",
                "Descirption",
                DateTime.Now.AddDays(12)
                ).Execute();

            ISeatTypeRepository seatTypes = new JsonSeatTypeRepository(
                new JsonParser<SeatType>(),
                new StorageOptions("SeatTypesJson")
                );

            var efRepo = new EFConcertRepository(new EventContext());

            var createSeatTypeCommanResult = new CreateSeatTypeCommand(seatTypes,
                createConcertCommandResult.CreatedAggregateRootId.Value,
                "first Sector",
                100,
                new Money("GEL", 20)
                ).Execute();
        }
    }
}
