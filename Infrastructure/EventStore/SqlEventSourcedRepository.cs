using Infrastructure.EventSourcedAggregateRoot;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EventStore
{
    public class SqlEventSourcedRepository<TAggregateRoot> :
        IEventSourcedRepository<TAggregateRoot> where TAggregateRoot : IEventSourcedAggregateRoot
    {
        public TAggregateRoot OfId(string id)
        {
            throw new NotImplementedException();
        }

        public void Store(TAggregateRoot root, string correlationId)
        {
            foreach (var uncommitedChange in root.UncommittedChanges())
            {
                var @event = Event.Build(uncommitedChange);
                //save to events table
            }
        }
    }
}
