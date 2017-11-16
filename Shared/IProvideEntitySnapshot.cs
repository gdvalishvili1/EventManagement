using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IProvideEntitySnapshot<TSnapshot>
    {
        TSnapshot Snapshot();
    }

    public class SnapshotProvider<TSnapshot>
    {
        IProvideEntitySnapshot<TSnapshot> _snapshotContainer;
        public SnapshotProvider(IProvideEntitySnapshot<TSnapshot> snapshotContainer)
        {
            _snapshotContainer = snapshotContainer;
        }

        public TSnapshot Provide()
        {
            return _snapshotContainer.Snapshot();
        }
    }
}
