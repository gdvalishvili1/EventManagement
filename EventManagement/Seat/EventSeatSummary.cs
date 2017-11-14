using EventManagement.ValueObjects;
using EventManagement.Venue;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class SeatTypeId : Identity
    {
        public SeatTypeId()
        {
        }

        public SeatTypeId(string id) : base(id)
        {
        }
    }
    public class SeatType : Entity
    {
        public SeatType(SeatTypeId id, EventId eventId, string name, int quantity, Money money)
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
            Money = money ?? throw new ArgumentNullException(nameof(money));
        }
        private EventId EventId { get; }
        public SeatTypeId Id { get; }
        private string Name { get; set; }
        private int Quantity { get; set; }
        private Money Money { get; set; }
    }

    public class SeatId : Identity
    {
        public SeatId()
        {
        }

        public SeatId(string id) : base(id)
        {
        }
    }

    public class Seat : Entity
    {
        public Seat(SeatId id, VenueId venueId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            VenueId = venueId ?? throw new ArgumentNullException(nameof(venueId));
        }

        public SeatId Id { get; }
        private VenueId VenueId { get; }
    }

    public class EventSeatSummary
    {

    }
}
