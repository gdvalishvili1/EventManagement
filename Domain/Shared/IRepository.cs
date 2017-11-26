using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot, IVersionedAggregateRoot
    {
        TAggregate OfId(string id);
        void Store(TAggregate aggregateRoot);
        void Delete(TAggregate aggregateRoot);
    }
}
