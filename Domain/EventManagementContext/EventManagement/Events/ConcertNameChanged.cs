using Shared;
using System;

namespace EventManagement.Events
{
    public class ConcertNameChanged : DomainEvent
    {
        public ConcertNameChanged(string aggregateRootId, string newName)
            : base(aggregateRootId, DateTime.Now)
        {
            NewName = newName;
        }

        public string NewName { get; }
    }
}
