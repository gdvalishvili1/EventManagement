using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IEventSourcedRepository<TAggregateRoot> where TAggregateRoot : IEventSourcedAggregateRoot
    {
        TAggregateRoot OfId(string id);
        void Store(TAggregateRoot eventSourced, string correlationId);
    }
}
