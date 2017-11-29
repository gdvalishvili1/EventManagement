using Shared;
using System;

namespace EventManagement.Events
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
