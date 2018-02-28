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
    public interface IRepository<TAggregate, TSnapshot> where TAggregate : AggregateRoot, IVersionedAggregateRoot, IProvideSnapshot<TSnapshot>
    {
        TAggregate OfId(string id);
        void Store(TAggregate aggregateRoot);
        void Delete(TAggregate aggregateRoot);
    }
}
