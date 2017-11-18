using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class ConcertSeatSummarySnapshot
    {
        public ConcertSeatSummarySnapshot(string id, string eventId, List<SeatType> seatTypes)
        {

        }
        public string Id { get; }

        public string ConcertId { get; }

        public List<SeatTypeSnapshot> SeatTypes { get; }
    }
}
