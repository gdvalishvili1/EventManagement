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

    public class Venue : AggregateRoot
    {
        public override string Identity => Id.Value;
        public VenueId Id { get; }
        private string Name { get; set; }
        private Address Location { get; set; }

        public Venue(VenueId id, string name, Address location)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name should not be empty or null", nameof(name));
            }

            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name;
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }
    }
}

