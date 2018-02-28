using Dapper;
using Infrastructure.Messaging;
using Shared;
using Shared.Json;
using System;
using System.Data.SqlClient;

namespace Infrastructure.Persistence
{
    public class SnapshotBasedJsonRepository<TAggregate, TSnapshot>
        where TAggregate : AggregateRoot, IVersionedAggregateRoot, IHasDomainEvents, IProvideSnapshot<TSnapshot>
        where TSnapshot : IRehydrateAggregate<TAggregate>
    {
        private JsonParser<TSnapshot> _jsonParser;
        private StorageOptions _options;

        public SnapshotBasedJsonRepository(JsonParser<TSnapshot> jsonParser, StorageOptions options)
        {
            _jsonParser = jsonParser;
            _options = options;
        }

        public TAggregate OfId(string id)
        {
            var existing = Get(id);
            var aggregateRoot = _jsonParser.FromJson(existing.Data).ToAggregate();
            aggregateRoot.SetVersion(existing.Version);

            return aggregateRoot;

        }

        public void Store(TAggregate aggregateRoot)
        {
            if (aggregateRoot.NewlyCreated())
            {
                Insert(aggregateRoot);
            }
            else
            {
                Update(aggregateRoot);
            }
        }

        private void Insert(TAggregate aggregateRoot)
        {
            var data = _jsonParser.AsJson(aggregateRoot.Snapshot());
            var eventEntry = new PersitedObjectContainer(Guid.Parse(aggregateRoot.Identity), data, 1);

            Execute(con =>
            {
                using (var tran = con.BeginTransaction())
                {
                    con.Execute($"insert into {_options.TableName} (Id,Data,Version) values ('{eventEntry.Id}','{eventEntry.Data}',{eventEntry.Version})", transaction: tran);

                    HandleEventInsert(aggregateRoot, con, tran);
                    tran.Commit();
                }
            });
        }

        private void Update(TAggregate aggregateRoot)
        {
            var entry = Get(aggregateRoot.Identity);
            if (entry.Version != aggregateRoot.Version())
            {
                throw new Exception("concurency exception");
            }

            var data = _jsonParser.AsJson(aggregateRoot.Snapshot());

            var updatedEntry = new PersitedObjectContainer(Guid.Parse(aggregateRoot.Identity), data, entry.Version + 1);

            Execute(con =>
            {
                using (var tran = con.BeginTransaction())
                {
                    con.Execute($"update {_options.TableName} set data='{updatedEntry.Data}',version={updatedEntry.Version}");
                    HandleEventInsert(aggregateRoot, con, tran);
                    tran.Commit();
                }
            });
        }

        private void HandleEventInsert(TAggregate aggregateRoot, SqlConnection con, SqlTransaction tran)
        {
            foreach (var evnt in aggregateRoot.UncommittedChanges())
            {
                var eventData = EventQueueItem.Serialize(evnt, typeof(TAggregate).Name, Guid.NewGuid());
                con.Execute($"insert into [Messaging].[EventQueue] values ('{eventData.AggregateRootId}','{eventData.CorrelationId}','{eventData.EventName}','{eventData.OccuredOn}','{eventData.Payload}','{eventData.AggregateName}')", transaction: tran);
            }
        }

        public void Delete(TAggregate aggregateRoot)
        {
            throw new NotImplementedException();
        }

        private PersitedObjectContainer Get(string id)
        {
            PersitedObjectContainer result = null;

            Execute((con) =>
            {
                result = con.QueryFirstOrDefault<PersitedObjectContainer>($"select * from {_options.TableName} where id = '{id}'");
            });

            return result;
        }

        private void Execute(Action<SqlConnection> action)
        {
            using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=EventManagement;Integrated Security=True;MultipleActiveResultSets=True"))
            {
                conn.Open();
                action(conn);
            }
        }
    }
}
