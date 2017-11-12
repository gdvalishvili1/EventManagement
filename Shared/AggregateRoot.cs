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
        int Version();
        void IncrementVersion();
        void SetVersion(int version);
    }

    public abstract class AggregateRoot : IEntity, IVersionedAggregateRoot
    {
        private int _version;
        public Identity Id { get; protected set; }

        int IVersionedAggregateRoot.Version()
        {
            return _version;
        }

        void IVersionedAggregateRoot.IncrementVersion()
        {
            _version++;
        }

        void IVersionedAggregateRoot.SetVersion(int version)
        {
            _version = version;
        }
    }
}
