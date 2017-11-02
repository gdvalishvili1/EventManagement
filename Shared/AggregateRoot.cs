using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public abstract class AggregateRoot<TId> : IEventSourcedAggregaterRoot
    {
        private readonly Dictionary<Type, Action<DomainEvent>> _registeredEvents;
        private readonly List<DomainEvent> _changes = new List<DomainEvent>();
        public TId Id { get; protected set; }
        public AggregateRoot()
        {
            _registeredEvents = new Dictionary<Type, Action<DomainEvent>>();
        }

        protected void ApplyChange(DomainEvent change)
        {
            _changes.Add(change);
            (this as IEventSourcedAggregaterRoot).Apply(change);
        }

        IEnumerable<DomainEvent> IEventSourcedAggregaterRoot.UncommittedChanges()
        {
            return _changes;
        }

        void IEventSourcedAggregaterRoot.Apply(List<DomainEvent> changes)
        {
            changes.ForEach(change => (this as IEventSourcedAggregaterRoot).Apply(change));
        }

        void IEventSourcedAggregaterRoot.Apply(DomainEvent change)
        {
            var whenMethod = GetType()
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name.Equals("On"))
            .Where(m => m.GetParameters()
            .SingleOrDefault(p => p.ParameterType.FullName.Equals(change.GetType().FullName)) != null);

            whenMethod.Single().Invoke(this, new object[] { change });
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }
    }

    public interface IEventSourcedAggregaterRoot
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
