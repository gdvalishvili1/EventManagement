using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public class ConcertId : Identity
    {
        public ConcertId()
        {
        }

        [JsonConstructor]
        public ConcertId(string value) : base(value)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
