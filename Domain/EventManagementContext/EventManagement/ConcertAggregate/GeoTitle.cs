using EventManagement.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    public class GeoTitle : Title
    {
        public GeoTitle(string value) : base(value)
        {
        }
    }
}
