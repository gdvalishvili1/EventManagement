using Events.EventManagement;
using System;

namespace EventManagement.EventHandlers
{
    public class ConcertCreatedProjector
    {
        public void Project(ConcertCreated @event)
        {
            Console.WriteLine($"concert registered {@event.Name}");
        }
    }
}
