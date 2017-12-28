using EventManagement.ConcertAggregate;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.Infrastructure.Persistence
{
    public class EFRepository<TAggregate, TDataModel, Tsnapshot> : IRepository<TAggregate>
        where TAggregate : AggregateRoot, IVersionedAggregateRoot, IProvideSnapshot<Tsnapshot>
        where Tsnapshot : class, new()
        where TDataModel : class, Idbentity
    {
        private readonly IAggregateFactory<TAggregate, Tsnapshot> _aggregateFactory;
        private readonly EventContext _db;
        public EFRepository(EventContext db, IAggregateFactory<TAggregate, Tsnapshot> aggregateFactory)
        {
            _db = db;
            _aggregateFactory = aggregateFactory;
        }
        public TAggregate OfId(string id)
        {
            var entity = _db.Set<TDataModel>().FirstOrDefault(x => x.Id == Guid.Parse(id));
            //return Tagg
            return _aggregateFactory.CreateFrom(new Tsnapshot());
        }

        public void Delete(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public void Store(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public void Update(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
