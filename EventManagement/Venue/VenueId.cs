using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Venue
{
    public class VenueId : Identity
    {
        public VenueId() { }
        [JsonConstructor]
        public VenueId(string value) : base(value)
        {
        }
    }
}
