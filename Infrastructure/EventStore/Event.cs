using Shared;
using Shared.Json;
using System;

namespace Infrastructure.EventStore
{
    public class Event
    {
        public Event(string aggregateRootId, DateTime occuredOn, string eventName, int version, string aggregateName, string payload)
        {
            AggregateRootId = Guid.Parse(aggregateRootId);
            OccuredOn = occuredOn;
            EventName = eventName;
            Version = version;
            AggregateName = aggregateName;
            Payload = payload;
        }
        protected Event()
        {

        }
        public Guid AggregateRootId { get; protected set; }
        public DateTime OccuredOn { get; protected set; }
        public string EventName { get; protected set; }
        public int Version { get; protected set; }
        public string Payload { get; protected set; }
        public string AggregateName { get; set; }

        public static Event Serialize(VersionedDomainEvent evnt, string aggregateName)
        {
            var parser = new JsonParser<VersionedDomainEvent>();

            return new Event(evnt.AggregateRootId, evnt.OccuredOn, evnt.EventType, evnt.Version, aggregateName, parser.AsJson(evnt));
        }

        public static VersionedDomainEvent DeSerialize(Event evnt)
        {
            var parser = new JsonParser<VersionedDomainEvent>();
            var obj = parser.FromJson(evnt.Payload);
            obj.AggregateRootId = evnt.AggregateRootId.ToString();
            obj.Version = evnt.Version;
            obj.OccuredOn = evnt.OccuredOn;
            obj.EventType = evnt.EventName;

            return obj;
        }
    }
}
