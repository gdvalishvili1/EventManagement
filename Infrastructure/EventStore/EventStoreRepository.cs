using EventStore.ClientAPI;
using Shared;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EventStore
{
    public class EventStoreRepository<TAggregateRoot> :
    IEventSourcedRepository<TAggregateRoot> where TAggregateRoot : Entity, IEventSourcedAggregateRoot
    {
        private string aggregateTypeName = typeof(TAggregateRoot).Name;
        public TAggregateRoot Load(string id)
        {
            var parser = new JsonParser<VersionedDomainEvent>();
            using (var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                connection.ConnectAsync().Wait();

                var events = GetEvents(StreamName(id)).Result;
                // var ev = events.Select(x => new Event(x.EventId.ToString(),x.)).ToList();
                //return CreateInstance(id, allEvents.Select(x => x.OriginalEvent.));
            }

            throw new Exception();
        }

        async Task<List<RecordedEvent>> GetEvents(string streamName)
        {
            long sliceStart = 0;
            var deserializedEvents = new List<RecordedEvent>();
            StreamEventsSlice slice;

            using (var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                await connection.ConnectAsync();
                do
                {
                    slice = await connection.ReadStreamEventsForwardAsync(streamName, sliceStart, 200, true);

                    deserializedEvents.AddRange(slice.Events.Select(e => e.Event));

                    sliceStart = slice.NextEventNumber;

                } while (!slice.IsEndOfStream);

            }
            return deserializedEvents;
        }

        private TAggregateRoot CreateInstance(string id, IEnumerable<VersionedDomainEvent> events)
        {
            var constructor = typeof(TAggregateRoot)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                new[]
            { typeof(string), typeof(IEnumerable<VersionedDomainEvent>) }, null);

            if (constructor == null)
            {
                throw new InvalidCastException("TAggregate must have .ctor(string, IEnumerable<VersionedDomainEvent>)");
            }

            return (TAggregateRoot)constructor.Invoke(new object[] { id, events });
        }
        public void Store(TAggregateRoot root)
        {
            var parser = new JsonParser<VersionedDomainEvent>();
            using (var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113)))
            {
                connection.ConnectAsync().Wait();

                var eventsToSave = root.UncommittedChanges()
                    .Select(x => Event.Serialize(x, aggregateTypeName))
                    .Select(x => new EventData(x.AggregateRootId, x.EventName,
                    true, Encoding.UTF8.GetBytes(x.Payload),
                    Encoding.UTF8.GetBytes(x.AggregateRootId.ToString())));

                connection.AppendToStreamAsync(StreamName(root.Identity),
                    ExpectedVersion.Any, eventsToSave).Wait();
            }
        }

        private string StreamName(string id)
        {
            return $"{aggregateTypeName}-{id}";
        }
    }
}
