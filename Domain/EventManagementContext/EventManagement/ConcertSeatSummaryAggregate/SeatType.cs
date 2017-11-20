using EventManagement.ValueObjects;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class SeatType : Entity, IProvideSnapshot<SeatTypeSnapshot>
    {
        private ConcertId EventId { get; }
        private ConcertSeatSummaryId ConcertSeatSummaryId { get; }
        public SeatTypeId Id { get; }

        private string Name { get; set; }

        private int Quantity { get; set; }

        private Money Price { get; set; }

        public override string Identity => Id.Value;

        public SeatType(ConcertSeatSummaryId concertSeatSummaryId, SeatTypeId id, ConcertId eventId, string name, int quantity, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("not be null or empty", nameof(name));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("must be greater than zero", nameof(name));
            }

            ConcertSeatSummaryId = concertSeatSummaryId ?? throw new ArgumentNullException(nameof(concertSeatSummaryId));
            Id = id;
            EventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            Name = name;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        SeatTypeSnapshot IProvideSnapshot<SeatTypeSnapshot>.Snapshot()
        {
            return new SeatTypeSnapshot(ConcertSeatSummaryId, Id.Value, Name, Quantity, Price);
        }
    }
}
