using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IEventSourcedRepository<TAggregateRoot> where TAggregateRoot : IEventSourcedAggregateRoot
    {
        TAggregateRoot Load(string id);
        void Store(TAggregateRoot eventSourced);
    }
}
