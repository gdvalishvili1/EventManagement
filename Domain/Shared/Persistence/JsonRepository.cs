using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Persistence
{
    public class JsonPrivateFieldsContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
    public class JsonParser<T>
    {
        public string AsJson(T aggregateRoot)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateFieldsContractResolver(),
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
            };
            var obj = JsonConvert.SerializeObject(aggregateRoot, settings);
            return obj;
        }

        public T FromJson(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            };
            var root = JsonConvert.DeserializeObject<T>(json, settings);

            return root;
        }
    }

    public class PersitedObjectContainer
    {
        public PersitedObjectContainer(Guid id, string data, int version)
        {
            Id = id;
            Data = data;
            Version = version;
        }
        public Guid Id { get; }
        public string Data { get; }
        public int Version { get; }
    }

    public class StorageOptions
    {
        public string TableName { get; set; }
        public StorageOptions(string tableName)
        {
            TableName = tableName;
        }
    }
    public class JsonRepository<TAggregate> where TAggregate : AggregateRoot, IVersionedAggregateRoot
    {
        private JsonParser<TAggregate> _jsonParser;
        private StorageOptions _options;

        public JsonRepository(JsonParser<TAggregate> jsonParser, StorageOptions options)
        {
            _jsonParser = jsonParser;
            _options = options;
        }

        public TAggregate ById(string id)
        {
            var existing = Get(id);
            var aggregateRoot = _jsonParser.FromJson(existing.Data);
            aggregateRoot.SetVersion(existing.Version);
            return aggregateRoot;
        }

        public void Insert(TAggregate aggregateRoot)
        {
            var data = _jsonParser.AsJson(aggregateRoot);
            var eventEntry = new PersitedObjectContainer(Guid.Parse(aggregateRoot.Identity), data, 1);

            Execute(con =>
            {
                using (var tran = con.BeginTransaction())
                {
                    con.Execute($"insert into {_options.TableName} (Id,Data,Version) values ('{eventEntry.Id}','{eventEntry.Data}',{eventEntry.Version})", transaction: tran);
                    //get aggregateroot s uncommited events and save to database using the same transaction
                    tran.Commit();
                }
            });
        }

        public void Update(TAggregate aggregateRoot)
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
