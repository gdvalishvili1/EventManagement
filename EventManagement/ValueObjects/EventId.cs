using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public class EventId : Identity
    {
        public EventId()
        {
        }

        [JsonConstructor]
        public EventId(string value) : base(value)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
