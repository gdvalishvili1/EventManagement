using EventManagement.ConcertAggregate;
using Shared.Json;
using Shared.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.Persistence
{
    public class JsonConcertRepository : JsonRepository<Concert>, IConcertRepository
    {
        public JsonConcertRepository(JsonParser<Concert> jsonParser, StorageOptions options)
            : base(jsonParser, options)
        {

        }
    }
}
