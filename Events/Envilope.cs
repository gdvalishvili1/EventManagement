using System;
using System.Collections.Generic;
using System.Text;

namespace Events
{
    public class Envilope
    {
        public Envilope(string aggregateRootId, DateTime date)
        {
            AggregateRootId = aggregateRootId;
            Date = date;
        }
        public string AggregateRootId { get; }
        public DateTime Date { get; }
        public string EventType { get; set; }
    }
}
