using EventManagement.ConcertAggregate;
using Shared;
using System;
using System.Linq;

namespace EventManagement.Infrastructure.Persistence
{
    public class EFConcertRepository : IConcertRepository, IDisposable
    {
        private readonly EventContext _db;
        public EFConcertRepository(EventContext db)
        {
            _db = db;
        }
        public Concert OfId(string id)
        {
            var concertEntity = _db.Concerts.FirstOrDefault(x => x.Id == Guid.Parse(id));
            var rehydratedConcert = new ConcertFactory().CreateFrom(concertEntity.RehydrateConcertSnapshot());
            return rehydratedConcert;
        }

        public void Delete(Concert aggregateRoot)
        {
            _db.Concerts.Remove(ConcertEntity.FromConcertSnapshot(ConcertSnapshot.CreateFrom(aggregateRoot)));
            _db.SaveChanges();
        }

        public void Store(Concert aggregateRoot)
        {
            if ((aggregateRoot as IHasDomainEvents).NewlyCreated())
            {
                var snapshot = ConcertSnapshot.CreateFrom(aggregateRoot);
                _db.Concerts.Add(ConcertEntity.FromConcertSnapshot(snapshot));
                _db.SaveChanges();
            }
            else
            {
                Update(aggregateRoot);
            }
        }

        private void Update(Concert aggregateRoot)
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
