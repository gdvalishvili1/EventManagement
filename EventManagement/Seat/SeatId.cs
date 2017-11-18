using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class SeatId : Identity
    {
        public SeatId()
        {
        }
        [JsonConstructor]
        public SeatId(string value) : base(value)
        {
        }
    }
}
