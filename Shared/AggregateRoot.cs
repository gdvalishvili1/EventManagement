using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public abstract class AggregateRoot<TId> : IEventSourcedAggregaterRoot
    {
        private readonly Dictionary<Type, Action<DomainEvent>> _registeredEvents;
        public AggregateRoot()
        {
            _registeredEvents = new Dictionary<Type, Action<DomainEvent>>();
        }
        private List<DomainEvent> _changes { get; set; } = new List<DomainEvent>();
        public TId Id { get; protected set; }

        protected void Emit(DomainEvent evnt)
        {
            _changes.Add(evnt);
        }

        List<DomainEvent> IEventSourcedAggregaterRoot.Changes()
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
            .Where(m => m.GetParameters().SingleOrDefault(p => p.ParameterType.FullName.Equals(change.GetType().FullName)) != null);
            whenMethod.Single().Invoke(this, new object[] { change });
        }
    }

    public interface IEventSourcedAggregaterRoot
    {
        List<DomainEvent> Changes();
        void Apply(List<DomainEvent> changes);
        void Apply(DomainEvent change);
    }

    public interface IStoreAggregateRootChanges
    {
        void StoreChanges();
    }
}
