using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class ConcertSeatSummarySnapshot
    {
        public ConcertSeatSummarySnapshot(string id, string concertId, List<SeatType> seatTypes)
        {
            Id = id;
            ConcertId = concertId;
            SeatTypes = seatTypes.Select(x => new SeatTypeSnapshotProvider(x).Snapshot).ToList();
        }
        public string Id { get; }

        public string ConcertId { get; }

        public IList<SeatTypeSnapshot> SeatTypes { get; }
    }
}
