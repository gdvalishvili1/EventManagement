using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot, IVersionedAggregateRoot
    {
        TAggregate ById(string id);
        void Insert(TAggregate aggregateRoot);
        void Update(TAggregate aggregateRoot);
        void Delete(TAggregate aggregateRoot);
    }
}
