using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class ConcertSeatSummarySnapshotProvider : SnapshotProvider<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummarySnapshotProvider(IProvideSnapshot<ConcertSeatSummarySnapshot> snapshotContainer)
            : base(snapshotContainer)
        {
        }
    }

    public class SeatTypeSnapshotProvider : SnapshotProvider<SeatTypeSnapshot>
    {
        public SeatTypeSnapshotProvider(IProvideSnapshot<SeatTypeSnapshot> snapshotContainer)
            : base(snapshotContainer)
        {
        }
    }
}
