using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.UnitTest
{
    public class DomainTest
    {
        public string EventNotRaisedMessage<T>() => $"{typeof(T).Name} event did not raised";
        public bool RaiseSingleEventOf<T>(IEventSourcedAggregateRoot eventSourcedAggregateRoot)
        {
            return eventSourcedAggregateRoot.UncommittedChanges().OfType<T>().SingleOrDefault() != null;
        }
    }
}
