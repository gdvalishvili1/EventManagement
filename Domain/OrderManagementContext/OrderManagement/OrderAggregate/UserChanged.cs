using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Domain.OrderAggregate
{
    public class UserChanged : VersionedDomainEvent
    {
        public string UserId { get; }
        public UserChanged(string userId)
        {
            UserId = userId;
        }
    }
}
