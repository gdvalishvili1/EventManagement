using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<DomainEvent> UncommittedChanges();
        void MarkChangesAsCommitted();
        void Apply(DomainEvent evnt);
        bool NewlyCreated();
    }
}
