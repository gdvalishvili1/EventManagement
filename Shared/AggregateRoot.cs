using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class Identity
    {
        public abstract Guid AsGuid();
    }
    public interface IVersionedAggregateRoot
    {
        int Version { get; }
        void IncrementVersion();
        void SetVersion(int version);
    }

    public abstract class AggregateRoot : IEntity, IVersionedAggregateRoot
    {
        public Identity Id { get; protected set; }

        public int Version { get; protected set; }

        public void IncrementVersion()
        {
            Version++;
        }

        public void SetVersion(int version)
        {
            Version = version;
        }
    }
}
