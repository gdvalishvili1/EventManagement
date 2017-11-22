using EventManagement.ConcertAggregate;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EventManagement.ValueObjects;

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

            var rehydratedConcert = new ConcertFactory().CreateFrom(new ConcertSnapshot
                    (new ConcertId(concertEntity.Id.ToString()),
                    concertEntity.Date,
                    concertEntity.Organizer,
                    concertEntity.Description,
                    concertEntity.TitleGeo,
                    concertEntity.TitleEng
                    )
                );

            return rehydratedConcert;
        }

        public void Delete(Concert aggregateRoot)
        {
            //delete whole aggregate data model from db
        }

        public void Insert(Concert aggregateRoot)
        {
            IProvideSnapshot<ConcertSnapshot> iprovideSnapshot = aggregateRoot;
            var snapshot = iprovideSnapshot.Snapshot();
            _db.Concerts.Add(new ConcertEntity
            {
                Id = snapshot.Id.AsGuid(),
                Date = snapshot.Date,
                Description = snapshot.Description,
                Organizer = snapshot.Description,
                TitleEng = snapshot.TitleEng,
                TitleGeo = snapshot.TitleGeo
            });
            _db.SaveChanges();
        }

        public void Update(Concert aggregateRoot)
        {
            IProvideSnapshot<ConcertSnapshot> iprovideSnapshot = aggregateRoot;
            var snapshot = iprovideSnapshot.Snapshot();
            var concertEntity = _db.Concerts.Find(aggregateRoot.Id.AsGuid());
            concertEntity.Date = snapshot.Date;
            concertEntity.Description = snapshot.Description;
            concertEntity.Organizer = snapshot.Organizer;
            concertEntity.TitleEng = snapshot.TitleEng;
            concertEntity.TitleGeo = snapshot.TitleGeo;

            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
