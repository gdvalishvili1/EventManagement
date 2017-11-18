using EventManagement.Concert;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Infrastructure;
using Shared;
using Shared.model;
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

            Concert concert = ConcertFactory.Create("Geo Title", "Eng Title", "Descirption", DateTime.Now.AddDays(12));

            var concerts = new ConcertRepository(new JsonParser<Concert>(), new StorageOptions("event_tbl"));
            concerts.Insert(concert);

            concert.AssignOrganizer("john");

            var eventSeatSummary = new ConcertSeatSummary(new ConcertSeatSummaryId(Guid.NewGuid().ToString()), concert.Id);

            eventSeatSummary.AddSeatType(
                new SeatType(new SeatTypeId(),
                new ConcertId(),
                "First Sector",
                100,
                new Money("GEL", 20))
                );

            eventSeatSummary.AddSeatType(
                new SeatType(new SeatTypeId(),
                new ConcertId(),
                "Second Sector",
                100,
                new Money("GEL", 10))
                );

            concert.AddEventSeatSummary(eventSeatSummary);

            var newconcert = concerts.ById(concert.Id.ToString());
            newconcert.ChangeConcertTitle("new geo title", "new engTitle");

            concerts.Update(newconcert);

            var new3 = concerts.ById(concert.Id.ToString());

            new3.AssignOrganizer("sxva");

            concerts.Update(new3);

            var new4 = concerts.ById(concert.Id.ToString());


            //IProvideEntitySnapshot<ConcertSnapshot> provideEntitySnapshot = concert;
            //ConcertSnapshot snapshot = provideEntitySnapshot.Snapshot();

            //using (EventContext c = new EventContext())
            //{
            //    c.Concerts.Add(new ConcertEntity
            //    {
            //        Id = snapshot.Id.AsGuid(),
            //        Date = snapshot.Date,
            //        Description = snapshot.Description,
            //        Organizer = snapshot.Description,
            //        TitleEng = snapshot.TitleEng,
            //        TitleGeo = snapshot.TitleGeo
            //    });
            //    c.SaveChanges();
            //}

            //using (EventContext c = new EventContext())
            //{
            //    var concertEntity = c.Concerts.FirstOrDefault(x => x.Id ==
            //    Guid.Parse("9B3F6FA7-C5FC-4523-9976-ABF005D3FF5A"));
            //    var rehidratedConcert = Concert.CreateFrom(new ConcertSnapshot
            //        (new EventId(concertEntity.Id.ToString()),
            //        concertEntity.Date,
            //        concertEntity.Organizer,
            //        concertEntity.Description,
            //        concertEntity.TitleGeo,
            //        concertEntity.TitleEng
            //        )
            //    );
            //}
        }

        public static T AggregateById<T>(string id, List<Infrastructure.EventStore.Event> changes)
            where T : IEventSourcedAggregateRoot
        {
            T root = (T)Activator.CreateInstance(typeof(T), true);
            root.Apply(changes.Select(x => x.Data).ToList());

            return root;
        }
    }
}
