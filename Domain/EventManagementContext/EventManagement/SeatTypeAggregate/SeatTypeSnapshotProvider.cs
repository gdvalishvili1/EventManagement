using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.SeatTypeAggregate
{
    public class SeatTypeSnapshotProvider : SnapshotProvider<SeatTypeSnapshot>
    {
        public SeatTypeSnapshotProvider(IProvideSnapshot<SeatTypeSnapshot> snapshotContainer)
            : base(snapshotContainer)
        {
        }
    }
}
