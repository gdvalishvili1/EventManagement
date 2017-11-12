using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{

    public class EventId : Identity
    {
        Guid Value { get; }
        public EventId(string value)
        {
            Value = Guid.Parse(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override Guid AsGuid()
        {
            return Value;
        }
    }
}
