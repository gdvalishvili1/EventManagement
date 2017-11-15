using EventManagement.ValueObjects;
using EventManagement.Venue;
using Newtonsoft.Json;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.Seat
{
    public class SeatTypeId : Identity
    {
        public SeatTypeId()
        {
        }
        [JsonConstructor]
        public SeatTypeId(string value) : base(value)
        {
        }
    }
    public class SeatType : Entity
    {
        public SeatType(SeatTypeId id, EventId eventId, string name, int quantity, Money price)
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
        private EventId EventId { get; }
        public SeatTypeId Id { get; }
        private string Name { get; set; }
        private int Quantity { get; set; }
        private Money Price { get; set; }

        public override string Identity => Id.Value;
    }

    public class SeatId : Identity
    {
        public SeatId()
        {
        }
        [JsonConstructor]
        public SeatId(string value) : base(value)
        {
        }
    }

    public class Seat : Entity
    {
        public Seat(SeatId id, VenueId venueId, SeatType seatTye)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            VenueId = venueId ?? throw new ArgumentNullException(nameof(venueId));
            SeatTye = seatTye ?? throw new ArgumentNullException(nameof(seatTye));
        }

        public SeatId Id { get; }

        public override string Identity => Id.Value;

        private VenueId VenueId { get; }
        private SeatType SeatTye { get; }
    }

    public class EventSeatSummaryId : Identity
    {
        public EventSeatSummaryId()
        {
        }
        [JsonConstructor]
        public EventSeatSummaryId(string value) : base(value)
        {
        }
    }

    public class EventSeatSummary : Entity
    {
        public EventSeatSummary(EventSeatSummaryId id, EventId eventId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            EventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            SeatTypes = new List<SeatType>();
        }

        [JsonConstructor]
        private EventSeatSummary(EventSeatSummaryId id, EventId eventId, List<SeatType> seatTypes) : this(id, eventId)
        {
            SeatTypes = seatTypes;
        }

        public EventSeatSummaryId Id { get; }

        public override string Identity => Id.Value;

        private EventId EventId { get; }
        private List<SeatType> SeatTypes { get; set; }

        public void AddSeatType(SeatType seatType)
        {
            if (seatType == null)
            {
                throw new ArgumentNullException(nameof(seatType));
            }

            if (SeatTypes.Any(x => x.Id == seatType.Id))
            {
                throw new Exception("seattype already added");
            }

            SeatTypes.Add(seatType);
        }
    }
}
