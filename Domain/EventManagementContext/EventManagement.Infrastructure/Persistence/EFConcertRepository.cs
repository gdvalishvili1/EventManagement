using EventManagement.ConcertAggregate;
using Shared;
using System;
using System.Linq;

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
        public TAggregate ById(string id)
        {
            var entity = _db.Set<TDataModel>().FirstOrDefault(x => x.Id == Guid.Parse(id));
            //return Tagg
            return _aggregateFactory.CreateFrom(new Tsnapshot());
        }

        public void Delete(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public void Insert(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public void Update(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
    public class EFConcertRepository : IConcertRepository, IDisposable
    {
        private readonly EventContext _db;
        public EFConcertRepository(EventContext db)
        {
            _db = db;
        }
        public Concert ById(string id)
        {
            var concertEntity = _db.Concerts.FirstOrDefault(x => x.Id == Guid.Parse(id));
            var rehydratedConcert = new ConcertFactory().CreateFrom(concertEntity.RehydrateCocnertSnapshot());
            return rehydratedConcert;
        }

        public void Delete(Concert aggregateRoot)
        {
            _db.Concerts.Remove(ConcertEntity.FromConcertSnapshot(ConcertSnapshot.CreateFrom(aggregateRoot)));
            _db.SaveChanges();
        }

        public void Insert(Concert aggregateRoot)
        {
            var snapshot = ConcertSnapshot.CreateFrom(aggregateRoot);
            _db.Concerts.Add(ConcertEntity.FromConcertSnapshot(snapshot));
            _db.SaveChanges();
        }

        public void Update(Concert aggregateRoot)
        {
            var snapshot = ConcertSnapshot.CreateFrom(aggregateRoot);
            var concertEntity = _db.Concerts.Find(aggregateRoot.Id.AsGuid());
            concertEntity.ModifyWithConcertSnapshot(snapshot);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
