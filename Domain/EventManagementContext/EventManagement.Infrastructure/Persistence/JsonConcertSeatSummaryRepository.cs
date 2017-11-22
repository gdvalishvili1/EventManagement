using EventManagement.ConcertSeatSummaryAggregate;
using Shared.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.Persistence
{
    public class JsonConcertSeatSummaryRepository : JsonRepository<ConcertSeatSummary>, IConcertSeatSummaryRepository
    {
        public JsonConcertSeatSummaryRepository(JsonParser<ConcertSeatSummary> jsonParser, StorageOptions options)
            : base(jsonParser, options)
        {
        }
    }
}
