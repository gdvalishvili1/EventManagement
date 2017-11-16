using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IHasDomainEvents
    {
        IEnumerable<DomainEvent> UncommittedChanges();
        void MarkChangesAsCommitted();
        void Emit(DomainEvent evnt);
    }

    public abstract class Identity : IEquatable<Identity>
    {
        public Guid AsGuid()
        {
            return Guid.Parse(Value);
        }
        public Identity()
        {
            this.Value = Guid.NewGuid().ToString();
        }

        [JsonConstructor]
        public Identity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("not be null or empty", nameof(value));
            }

            this.Value = value;
        }

        public string Value { get; }

        public bool Equals(Identity id)
        {
            if (object.ReferenceEquals(this, id)) return true;
            if (object.ReferenceEquals(null, id)) return false;
            return this.Value.Equals(id.Value);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() + this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public interface IVersionedAggregateRoot
    {
        int Version();
        void IncrementVersion();
        void SetVersion(int version);
    }

    public abstract class AggregateRoot : Entity, IVersionedAggregateRoot, IHasDomainEvents
    {
        private int _version;

        private IList<DomainEvent> _events { get; }

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

        IEnumerable<DomainEvent> IHasDomainEvents.UncommittedChanges()
        {
            return _events;
        }

        void IHasDomainEvents.MarkChangesAsCommitted()
        {
            _events.Clear();
        }

        void IHasDomainEvents.Emit(DomainEvent evnt)
        {
            _events.Add(evnt);
        }

        protected void Emit(DomainEvent evnt)
        {
            (this as IHasDomainEvents).Emit(evnt);
        }
    }
}
