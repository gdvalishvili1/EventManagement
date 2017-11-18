using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IHasDomainEvents
    {
        IEnumerable<DomainEvent> UncommittedChanges();
        void MarkChangesAsCommitted();
        void Emit(DomainEvent evnt);
    }
}
