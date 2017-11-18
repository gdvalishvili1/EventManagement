using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IEventSourcedAggregateRoot
    {
        IEnumerable<DomainEvent> UncommittedChanges();
        void MarkChangesAsCommitted();
        void Apply(List<DomainEvent> changes);
        void Apply(DomainEvent change);
    }
}
