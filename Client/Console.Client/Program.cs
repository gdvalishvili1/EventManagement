using EventManagement.Application.UseCases.CreateNewConcert;
using EventManagement.Application.UseCases.CreateSeatType;
using EventManagement.ConcertAggregate;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Persistence;
using EventManagement.SeatTypeAggregate;
using Infrastructure.EventStore;
using OrderManagement.OrderAggregate;
using Shared.Json;
using Shared.Models.Money;
using Shared.Persistence;
using System;

namespace ConsoleTesting
{

    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order(new OrderId(), "123");

            var orders = new SqlEventSourcedRepository<Order>();

            var order1 = orders.Load("43ABFC31-A133-4317-AD97-6DE4DF4AD90F");

            IConcertRepository concerts = new JsonConcertRepository(
               new JsonParser<Concert>(),
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
