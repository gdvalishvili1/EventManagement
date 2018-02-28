using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public interface IRehydrateAggregate<TAggregate>
    {
        TAggregate ToAggregate();
    }
}
