using EventManagement.ConcertSeatSummaryAggregate;
using EventManagement.Seat;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagement.Events
{
    public class ConcertSeatSummaryAdded : DomainEvent
    {
        public ConcertSeatSummaryAdded(ConcertSeatSummarySnapshot snapshot, string aggregateRootId)
            : base(aggregateRootId, DateTime.Now)
        {
            Id = snapshot.Id;
            ConcertId = snapshot.ConcertId;
            SeatTypes = snapshot.SeatTypes;
        }
        public IEnumerable<SeatTypeSnapshot> SeatTypes { get; set; }
        public string Id { get; }
        public string ConcertId { get; }
    }
}
