using EventManagement.ConcertAggregate;
using EventManagement.ConcertSeatSummaryAggregate;
using EventManagement.Infrastructure;
using EventManagement.Infrastructure.Persistence;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Infrastructure;
using Shared;
using Shared.model;
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

            Concert concert = new ConcertFactory().Create(
                "Geo Title",
                "Eng Title",
                "Descirption",
                DateTime.Now.AddDays(12)
                );

            IConcertSeatSummaryRepository concertSeatSummaries = new JsonConcertSeatSummaryRepository(
                new JsonParser<ConcertSeatSummary>(),
                new StorageOptions("concertSeatSummary_tbl")
                );

            IConcertRepository concerts = new JsonConcertRepository(
                new JsonParser<Concert>(),
                new StorageOptions("event_tbl")
                );

            concerts.Insert(concert);

            var concertSeatSummary = new ConcertSeatSummary(
                new ConcertSeatSummaryId(Guid.NewGuid().ToString()),
                concert.Id
                );

            concertSeatSummary.AddNewSeatType(
                concertSeatSummary.CreateNewSeatType("first Sector", 100, new Money("GEL", 20))
                );

            concertSeatSummary.AddNewSeatType(
                concertSeatSummary.CreateNewSeatType("Second Sector", 100, new Money("GEL", 10))
                );

            concertSeatSummaries.Insert(concertSeatSummary);


            var efRepo = new EFConcertRepository(new EventContext());
            efRepo.Insert(concert);


            var concertFromEf = efRepo.ById(concert.Identity);
        }
    }
}
