using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Venue
{
    public class VenueId : Identity
    {
        public VenueId()
        {
        }

        public VenueId(string id) : base(id)
        {
        }
    }
    public class Venue : AggregateRoot
    {
        private string Name { get; set; }
        private Address Location { get; set; }

        public Venue(VenueId id, string name, Address location) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name should not be empty or null", nameof(name));
            }
            Name = name;
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }
    }
}

