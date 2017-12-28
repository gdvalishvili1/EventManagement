using Shared;
using System;

namespace EventManagement.Domain.ConcertAggregate.Events
{
    public class ConcertNameChanged : DomainEvent
    {
        public ConcertNameChanged(string newName)
        {
            NewName = newName;
        }

        public string NewName { get; }
    }
}
