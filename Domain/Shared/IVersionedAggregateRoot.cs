using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IVersionedAggregateRoot
    {
        int Version();
        void IncrementVersion();
        void SetVersion(int version);
    }
}
