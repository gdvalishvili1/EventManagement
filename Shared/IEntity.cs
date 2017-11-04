using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IEntity<TId>
    {
        TId Id { get; }
    }
}
