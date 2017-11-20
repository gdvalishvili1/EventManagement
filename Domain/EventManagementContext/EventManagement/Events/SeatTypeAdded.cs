using EventManagement.ConcertSeatSummaryAggregate;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Events
{
    public class SeatTypeAdded : DomainEvent
    {
        public SeatTypeAdded(SeatTypeSnapshot snapshot, ConcertId concertId, string aggregateRootId)
            : base(aggregateRootId, DateTime.Now)
        {
            Id = snapshot.Id;
            SeatType = snapshot;
            ConcertId = concertId.Value;
        }
        public SeatTypeSnapshot SeatType { get; }
        public string Id { get; }
        public string ConcertId { get; }
    }
}
