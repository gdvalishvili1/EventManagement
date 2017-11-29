using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IEventSourcedAggregateRoot
    {
        int Version { get; }
        IEnumerable<VersionedDomainEvent> UncommittedChanges();
        void MarkChangesAsCommitted();
        void Apply(List<VersionedDomainEvent> changes);
        void Apply(VersionedDomainEvent change);
    }
}
