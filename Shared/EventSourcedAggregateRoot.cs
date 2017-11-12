using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public abstract class EventSourcedAggregateRoot<TId> : IEventSourcedAggregateRoot, IEntity
    {
        private readonly Dictionary<Type, Action<DomainEvent>> _registeredEvents;
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();
        public TId Id { get; protected set; }
        public EventSourcedAggregateRoot()
        {
            _registeredEvents = new Dictionary<Type, Action<DomainEvent>>();
        }

        protected void ApplyChange(DomainEvent change)
        {
            _changes.Add(change);
            (this as IEventSourcedAggregateRoot).Apply(change);
        }

        IEnumerable<DomainEvent> IEventSourcedAggregateRoot.UncommittedChanges()
        {
            return _changes;
        }

        void IEventSourcedAggregateRoot.Apply(List<DomainEvent> changes)
        {
            changes.ForEach(change => (this as IEventSourcedAggregateRoot).Apply(change));
        }

        void IEventSourcedAggregateRoot.Apply(DomainEvent change)
        {
            var whenMethod = GetType()
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name.Equals("On"))
            .Where(m => m.GetParameters()
            .SingleOrDefault(p => p.ParameterType.FullName.Equals(change.GetType().FullName)) != null);

            whenMethod.Single().Invoke(this, new object[] { change });
        }

        void IEventSourcedAggregateRoot.MarkChangesAsCommitted()
        {
            _changes.Clear();
        }
    }

    public interface IEventSourcedAggregateRoot
    {
        IEnumerable<DomainEvent> UncommittedChanges();
        void Apply(List<DomainEvent> changes);
        void Apply(DomainEvent change);
        void MarkChangesAsCommitted();
    }

    public interface IStoreAggregateRootChanges
    {
        void StoreChanges();
    }
}
