using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class ConcertSeatSummarySnapshotProvider : SnapshotProvider<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummarySnapshotProvider(IProvideSnapshot<ConcertSeatSummarySnapshot> snapshotContainer)
            : base(snapshotContainer)
        {
        }
    }
}
