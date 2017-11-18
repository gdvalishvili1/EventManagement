using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IProvideSnapshot<TSnapshot>
    {
        TSnapshot Snapshot();
    }
}
