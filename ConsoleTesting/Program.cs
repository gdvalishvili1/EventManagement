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

namespace ConsoleTesting
{
    public class JsonPrivateFieldsContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
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

            var concert = new Concert(
                new EventId(Guid.NewGuid().ToString()),
                new EventTitleSummary(new GeoTitle("Geo Title")).WithAnotherTitle(new EngTitle("Eng Title")),
                new EventDescription(DateTime.Now.AddDays(15), "Description")
                );

            concert.AssignOrganizer("john");

            var str = AsJson(concert);
            var obj = FromJson<Concert>(str);
        }

        public static string AsJson<T>(T aggregateRoot) where T : IAggregateRoot
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateFieldsContractResolver(),
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
            };
            var c = JsonConvert.SerializeObject(aggregateRoot, settings);
            return c;
        }

        public static T FromJson<T>(string json) where T : IAggregateRoot
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            };
            var root = JsonConvert.DeserializeObject<T>(json, settings);
            return root;
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
