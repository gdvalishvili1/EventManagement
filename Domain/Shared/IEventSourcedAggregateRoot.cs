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
        void Apply(IEnumerable<VersionedDomainEvent> changes);
        void Apply(VersionedDomainEvent change);
    }
}
