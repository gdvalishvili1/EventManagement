using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Domain.ConcertAggregate
{
    public class EventOrganizer : ValueObject
    {
        public EventOrganizer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name shouold not null or empty", nameof(name));
            }

            Name = name;
        }

        public string Name { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
