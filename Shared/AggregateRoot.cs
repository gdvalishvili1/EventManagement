using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IAggregateRoot
    { }

    public abstract class AggregateRoot<TId> : IEntity<TId>, IAggregateRoot
    {
        public TId Id { get; protected set; }
    }
}
