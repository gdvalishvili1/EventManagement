using Events.EventManagement;
using System;

namespace EventManagement.EventHandlers
{
    public class ConcertCreatedEventHandler
    {
        public void Handle(ConcertCreated @event)
        {
            Console.WriteLine($"concert registered {@event.Name}");
        }
    }
}
