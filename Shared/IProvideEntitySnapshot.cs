using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class SnapshotProvider<TSnapshot>
    {
        IProvideSnapshot<TSnapshot> _snapshotContainer;
        public SnapshotProvider(IProvideSnapshot<TSnapshot> snapshotContainer)
        {
            _snapshotContainer = snapshotContainer;
        }

        public TSnapshot Provide()
        {
            return _snapshotContainer.Snapshot();
        }
    }
}
