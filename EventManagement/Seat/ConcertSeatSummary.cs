using EventManagement.Entities;
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

    public class ConcertSeatSummaryId : Identity
    {
        public ConcertSeatSummaryId()
        {
        }
        [JsonConstructor]
        public ConcertSeatSummaryId(string value) : base(value)
        {
        }
    }

    public class SeatTypeSnapshot
    {
        public SeatTypeSnapshot(string id, string name, int quantity, Money money)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = Tuple.Create(money.Currency, money.Amount);
        }

        public string Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public Tuple<string, decimal> Price { get; }
    }

    public class ConcertSeatSummarySnapshotProvider : SnapshotProvider<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummarySnapshotProvider(IProvideEntitySnapshot<ConcertSeatSummarySnapshot> snapshotContainer)
            : base(snapshotContainer)
        {
        }
    }
    public class ConcertSeatSummarySnapshot
    {
        public ConcertSeatSummarySnapshot(string id, string eventId, List<SeatType> seatTypes)
        {

        }
        public string Id { get; }

        public string ConcertId { get; }

        public List<SeatTypeSnapshot> SeatTypes { get; }
    }

    public class ConcertSeatSummary : Entity, IProvideEntitySnapshot<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummaryId Id { get; }

        public override string Identity => Id.Value;

        private ConcertId ConcertId { get; }

        private List<SeatType> SeatTypes { get; set; }

        public ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId eventId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            SeatTypes = new List<SeatType>();
        }

        [JsonConstructor]
        private ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId eventId, List<SeatType> seatTypes) : this(id, eventId)
        {
            SeatTypes = seatTypes;
        }

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

        ConcertSeatSummarySnapshot IProvideEntitySnapshot<ConcertSeatSummarySnapshot>.Snapshot()
        {
            return new ConcertSeatSummarySnapshot(Id.Value, ConcertId.Value, SeatTypes);
        }
    }
}
