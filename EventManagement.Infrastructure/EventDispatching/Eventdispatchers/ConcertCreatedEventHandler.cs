using EventManagement.EventHandlers;
using EventManagement.Infrastructure.EventDispatching;
using Events.EventManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EventDispatching.EventDispatchers
{
    public class ConcertCreatedEventDispatcher : IHandle<ConcertCreated>
    {
        public void Handle(ConcertCreated @event)
        {
            new ConcertCreatedEventHandler().Handle(@event);
        }
    }
}
