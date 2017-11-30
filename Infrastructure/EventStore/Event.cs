using Shared;
using Shared.Json;
using System;

namespace Infrastructure.EventStore
{
    public class Event
    {
        public Event(string aggregateRootId, DateTime occuredOn, string eventName, string payload)
        {
            AggregateRootId = aggregateRootId;
            OccuredOn = occuredOn;
            EventName = eventName;
            Payload = payload;
        }
        public string AggregateRootId { get; }
        public DateTime OccuredOn { get; }
        public string EventName { get; }
        public int Version { get; }
        public string Payload { get; }

        public static Event Build(VersionedDomainEvent evnt)
        {
            var parser = new JsonParser<VersionedDomainEvent>();

            return new Event(evnt.AggregateRootId, evnt.OccuredOn, evnt.EventType, parser.AsJson(evnt));
        }
    }
}
