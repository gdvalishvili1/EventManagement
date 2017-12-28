using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Persistence
{
    public class JsonRepository<TAggregate> where TAggregate : AggregateRoot, IVersionedAggregateRoot, IHasDomainEvents
    {
        private JsonParser<TAggregate> _jsonParser;
        private StorageOptions _options;

        public JsonRepository(JsonParser<TAggregate> jsonParser, StorageOptions options)
        {
            _jsonParser = jsonParser;
            _options = options;
        }

        public TAggregate OfId(string id)
        {
            var existing = Get(id);
            var aggregateRoot = _jsonParser.FromJson(existing.Data);
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
            var data = _jsonParser.AsJson(aggregateRoot);
            var eventEntry = new PersitedObjectContainer(Guid.Parse(aggregateRoot.Identity), data, 1);

            Execute(con =>
            {
                using (var tran = con.BeginTransaction())
                {
                    con.Execute($"insert into {_options.TableName} (Id,Data,Version) values ('{eventEntry.Id}','{eventEntry.Data}',{eventEntry.Version})", transaction: tran);

                    foreach (var evnt in aggregateRoot.UncommittedChanges())
                    {
                        //insert into events table in same transaction
                    }
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

            var data = _jsonParser.AsJson(aggregateRoot);

            var updatedEntry = new PersitedObjectContainer(Guid.Parse(aggregateRoot.Identity), data, entry.Version + 1);

            Execute(con =>
            {
                con.Execute($"update {_options.TableName} set data='{updatedEntry.Data}',version={updatedEntry.Version}");
            });
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
