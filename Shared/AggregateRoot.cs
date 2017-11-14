using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class Identity : IEquatable<Identity>
    {
        public Guid AsGuid()
        {
            return Guid.Parse(Id);
        }
        public Identity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Identity(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("not be null or empty", nameof(id));
            }

            this.Id = id;
        }

        public string Id { get; set; }

        public bool Equals(Identity id)
        {
            if (object.ReferenceEquals(this, id)) return true;
            if (object.ReferenceEquals(null, id)) return false;
            return this.Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() + this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }
    }

    public interface IVersionedAggregateRoot
    {
        int Version();
        void IncrementVersion();
        void SetVersion(int version);
    }

    public abstract class AggregateRoot : Entity, IVersionedAggregateRoot
    {
        private int _version;
        public Identity Id { get; protected set; }
        public AggregateRoot(Identity id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        public AggregateRoot()
        {

        }
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
