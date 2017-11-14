using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public class EventId : Identity
    {
        public EventId(string id) : base(id)
        {

        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
