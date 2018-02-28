using EventManagement.ConcertAggregate;
using Infrastructure.Persistence;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.Persistence
{
    public class JsonConcertRepository : SnapshotBasedJsonRepository<Concert, ConcertSnapshot>, IConcertRepository
    {
        public JsonConcertRepository(JsonParser<ConcertSnapshot> jsonParser, StorageOptions options)
            : base(jsonParser, options)
        {

        }
    }
}
