using EventStore.ClientAPI;
using Shared;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Infrastructure.EventStore
{
    public class SqlEventSourcedRepository<TAggregateRoot> :
        IEventSourcedRepository<TAggregateRoot> where TAggregateRoot : IEventSourcedAggregateRoot
    {
        private string aggregateTypeName = typeof(TAggregateRoot).Name;
        public TAggregateRoot Load(string id)
        {
            using (var context = new EventStoreDbContext())
            {
                var deserialized = context.Events
                        .Where(x => x.AggregateRootId == Guid.Parse(id) && x.AggregateName == aggregateTypeName)
                        .OrderBy(x => x.Version)
                        .Select(x=>Event.DeSerialize(x.Payload))
                        .ToList();

                return CreateInstance(id, deserialized);
            }
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
            using (var context = new EventStoreDbContext())
            {
                foreach (var uncommitedChange in root.UncommittedChanges())
                {
                    var @event = Event.Serialize(uncommitedChange, aggregateTypeName);
                    context.Events.Add(@event);
                }
                context.SaveChanges();
            }
        }
    }
}
