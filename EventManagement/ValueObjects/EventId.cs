using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public class EventId
    {
        public EventId(string id = null)
        {
            this.Value = id == null ? Guid.NewGuid() : Guid.Parse(id);
        }
        protected Guid Value { get; set; }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
