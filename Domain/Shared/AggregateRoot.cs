using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Shared
{
    public abstract class AggregateRoot : Entity, IVersionedAggregateRoot, IHasDomainEvents
    {
        private int _version;

        private IList<DomainEvent> _events { get; } = new List<DomainEvent>();

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

        IReadOnlyList<DomainEvent> IHasDomainEvents.UncommittedChanges()
        {
            return new ReadOnlyCollection<DomainEvent>(_events);
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
