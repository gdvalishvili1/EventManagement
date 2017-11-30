using EventManagement;
using EventManagement.Application.UseCases.CreateNewConcert;
using EventManagement.Application.UseCases.CreateSeatType;
using EventManagement.ConcertAggregate;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Persistence;
using EventManagement.Seat;
using EventManagement.SeatTypeAggregate;
using EventManagement.ValueObjects;
using Infrastructure;
using Infrastructure.EventDispatching;
using Infrastructure.EventSourcedAggregateRoot;
using Infrastructure.EventStore;
using Newtonsoft.Json;
using OrderManagement.OrderAggregate;
using Shared;
using Shared.Json;
using Shared.model;
using Shared.Models.Money;
using Shared.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTesting
{

    class Program
    {
        static void Main(string[] args)
        {
            //var eventDispatcher = new EventDispatcher<DomainEvent>();
            //eventDispatcher.RegisterHandlers(typeof(IEventManagementContext).Assembly);
            //var inMemoryEventStore = new InMemoryEventStore(eventDispatcher);

            //var concert = new Concert(
            //    new EventId(Guid.NewGuid().ToString()),
            //    new EventTitleSummary("the eminem show", ""),
            //    new EventDescription(
            //        DateTime.Now.AddDays(24),
            //        "bla bla bla")
            //    );

            //IProvideEntityState<ConcertSnapshot> concertDto = concert;
            //concertDto.Get();

            //concert.AssignOrganizer("mp");
            //concert.ChangeConcertName("new name1");

            //new StoreAggregateRootChanges(concert, inMemoryEventStore).StoreChanges();

            //var priviousEvents = inMemoryEventStore.ChangesFor(concert.Id.ToString());
            //var appliedConcert = AggregateById<Concert>(concert.Id.ToString(), priviousEvents.ToList());

            //appliedConcert.AssignOrganizer("name1");
            //appliedConcert.ChangeConcertName("name2");

            //new StoreAggregateRootChanges(appliedConcert, inMemoryEventStore).StoreChanges();

            //var priviousEvents1 = inMemoryEventStore.ChangesFor(appliedConcert.Id.ToString());
            //var appliedConcert2 = AggregateById<Concert>(appliedConcert.Id.ToString(), priviousEvents1.ToList());


            var order = new Order(new OrderId(), "123");

            var repo = new SqlEventSourcedRepository<Order>();

            var order1 = repo.OfId("43ABFC31-A133-4317-AD97-6DE4DF4AD90F");

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
