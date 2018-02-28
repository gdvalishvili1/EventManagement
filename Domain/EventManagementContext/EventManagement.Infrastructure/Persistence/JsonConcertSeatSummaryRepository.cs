using EventManagement.SeatTypeAggregate;
using Infrastructure.Persistence;
using Shared.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure.Persistence
{
    public class JsonSeatTypeRepository : JsonRepository<SeatType>, ISeatTypeRepository
    {
        public JsonSeatTypeRepository(JsonParser<SeatType> jsonParser, StorageOptions options)
            : base(jsonParser, options)
        {
        }
    }
}
