using EventManagement.Entities;
using Infrastructure.EventDispatching;
using Infrastructure.EventSourcedAggregateRoot;
using Infrastructure.EventStore;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using EventManagement;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Infrastructure;
using EventManagement.Factories;

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

            concert.AssignOrganizer("john");

            var concerts = new ConcertRepository(new JsonParser<Concert>(), new StorageOptions("event_tbl"));
            concerts.Insert(concert);

            var newconcert = concerts.ById(concert.Id.ToString());
            newconcert.ChangeConcertTitle("new geo title", "new engTitle");

            concerts.Update(newconcert);

            var new3 = concerts.ById(concert.Id.ToString());

            new3.AssignOrganizer("sxva");

            concerts.Update(new3);

            var new4 = concerts.ById(concert.Id.ToString());


            IProvideEntitySnapshot<ConcertSnapshot> provideEntitySnapshot = concert;
            var dto = provideEntitySnapshot.Snapshot();
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
