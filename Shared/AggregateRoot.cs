using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class AggregateRoot<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; }
    }
}
