using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public abstract class EventSourcedAggregateRoot<TId> : Entity, IEventSourcedAggregateRoot where TId : class, new()
    {
        private readonly Dictionary<Type, Action<VersionedDomainEvent>> _registeredEvents;
        private readonly List<VersionedDomainEvent> _changes = new List<VersionedDomainEvent>();
        public TId Id { get; protected set; }

        public int Version { get; protected set; }

        public EventSourcedAggregateRoot()
        {
            _registeredEvents = new Dictionary<Type, Action<VersionedDomainEvent>>();
        }

        protected void ApplyChange(VersionedDomainEvent change)
        {
            _changes.Add(change);
            change.Version = Version + 1;
            change.AggregateRootId = Identity;
            change.OccuredOn = DateTime.Now;
            change.EventType = change.GetType().Name;
            Version = change.Version;

            (this as IEventSourcedAggregateRoot).Apply(change);
        }

        IEnumerable<VersionedDomainEvent> IEventSourcedAggregateRoot.UncommittedChanges()
        {
            return _changes;
        }

        void IEventSourcedAggregateRoot.MarkChangesAsCommitted()
        {
            _changes.Clear();
        }
        protected void Load(IEnumerable<VersionedDomainEvent> history)
        {
            (this as IEventSourcedAggregateRoot).Apply(history);
        }
        void IEventSourcedAggregateRoot.Apply(IEnumerable<VersionedDomainEvent> changes)
        {
            foreach (var change in changes)
            {
                (this as IEventSourcedAggregateRoot).Apply(change);
                Version = change.Version;
            }
        }

        void IEventSourcedAggregateRoot.Apply(VersionedDomainEvent change)
        {
            var onMethod = GetType()
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name.Equals("On"))
            .Where(m => m.GetParameters()
            .SingleOrDefault(p => p.ParameterType.FullName.Equals(change.GetType().FullName)) != null);
            onMethod.Single().Invoke(this, new object[] { change });
        }
    }
}
