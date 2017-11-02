using EventManagement.EventHandlers;
using EventManagement.Infrastructure.EventDispatching;
using Messages.Events.EventManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EventDispatching.EventDispatchers
{
    public class ConcertCreatedEventHandler : IHandle<ConcertCreated>
    {
        public void Handle(ConcertCreated @event)
        {
            new ConcertCreatedProjector().Project(@event);
        }
    }
}
