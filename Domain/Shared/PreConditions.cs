using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public static class PreConditions
    {
        public static void NotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
        }
    }
}
