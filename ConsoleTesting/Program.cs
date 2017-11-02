using EventManagement;
using EventManagement.Infrastructure.EventDispatching;
using EventManagement.Infrastructure.EventSourcedAggregateRoot;
using EventManagement.Infrastructure.EventStore;
using Messages;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventDispatcher = new EventDispatcher<DomainEvent>();
            eventDispatcher.RegisterHandlers(typeof(DomainEvent).Assembly);
            var inMemoryEventStore = new InMemoryEventStore(eventDispatcher);

            var concert = new Concert(new EventDescription("joni", DateTime.Now.AddDays(24)));
            concert.AssignOrganizer("jimi");
            concert.ChangeConcertName("axali saxeliaaa");

            new StoreAggregateRootChanges(concert, inMemoryEventStore).StoreChanges();

            var priviousEvents = inMemoryEventStore.ChangesFor(concert.Id.ToString());
            var appliedConcert = AggregateById<Concert>(concert.Id.ToString(), priviousEvents.ToList());

            appliedConcert.AssignOrganizer("1222");
            appliedConcert.ChangeConcertName("23232");

            new StoreAggregateRootChanges(appliedConcert, inMemoryEventStore).StoreChanges();

            var priviousEvents1 = inMemoryEventStore.ChangesFor(appliedConcert.Id.ToString());
            var appliedConcert2 = AggregateById<Concert>(appliedConcert.Id.ToString(), priviousEvents1.ToList());
        }

        public static T AggregateById<T>(string id, List<Event> changes) where T : IEventSourcedAggregaterRoot
        {
            T root = (T)Activator.CreateInstance(typeof(T), true);
            root.Apply(changes.Select(x => x.Data).ToList());

            return root;
        }
    }
}
