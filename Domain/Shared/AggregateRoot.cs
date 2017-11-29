using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        void IHasDomainEvents.Apply(DomainEvent evnt)
        {
            _events.Add(evnt);
        }

        protected void Apply(DomainEvent evnt)
        {
            evnt.AggregateRootId = Identity;
            evnt.DateOccuredOn = DateTime.Now;
            (this as IHasDomainEvents).Apply(evnt);
        }

        bool IHasDomainEvents.NewlyCreated()
        {
            return _events.Any(x => x is ICreateEvent);
        }
    }
}
