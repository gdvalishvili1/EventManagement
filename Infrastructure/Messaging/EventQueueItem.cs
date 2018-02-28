using Shared;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Messaging
{
    public class EventQueueItem
    {
        public EventQueueItem(string aggregateRootId, DateTime occuredOn, string eventName, string aggregateName, string payload, Guid correlationId)
        {
            AggregateRootId = Guid.Parse(aggregateRootId);
            OccuredOn = occuredOn;
            EventName = eventName;
            AggregateName = aggregateName;
            Payload = payload;
            CorrelationId = correlationId;
        }
        protected EventQueueItem()
        {

        }
        public Guid AggregateRootId { get; protected set; }
        public Guid CorrelationId { get; protected set; }
        public DateTime OccuredOn { get; protected set; }
        public string EventName { get; protected set; }
        public string Payload { get; protected set; }
        public string AggregateName { get; set; }

        public static EventQueueItem Serialize(DomainEvent evnt, string aggregateName, Guid correlationId)
        {
            var parser = new JsonParser<DomainEvent>();

            return new EventQueueItem(evnt.AggregateRootId, evnt.OccuredOn, evnt.EventType, aggregateName, parser.AsJson(evnt), correlationId);
        }

        public static VersionedDomainEvent DeSerialize(string payload)
        {
            var parser = new JsonParser<VersionedDomainEvent>();
            var obj = parser.FromJson(payload);
            return obj;
        }
    }

}
