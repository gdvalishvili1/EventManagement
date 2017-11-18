using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class SeatTypeId : Identity
    {
        public SeatTypeId()
        {
        }
        [JsonConstructor]
        public SeatTypeId(string value) : base(value)
        {
        }
    }
}
