using EventManagement.ValueObjects;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class SeatType : Entity
    {
        private ConcertId EventId { get; }

        public SeatTypeId Id { get; }

        private string Name { get; set; }

        private int Quantity { get; set; }

        private Money Price { get; set; }

        public override string Identity => Id.Value;

        public SeatType(SeatTypeId id, ConcertId eventId, string name, int quantity, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("not be null or empty", nameof(name));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("must be greater than zero", nameof(name));
            }

            Id = id;
            EventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            Name = name;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }
    }
}
