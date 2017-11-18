using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class ConcertSeatSummaryId : Identity
    {
        public ConcertSeatSummaryId()
        {
        }
        [JsonConstructor]
        public ConcertSeatSummaryId(string value) : base(value)
        {
        }
    }
}
