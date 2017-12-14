using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Date
{
    public class SystemDate : ISystemDate
    {
        private readonly DateTime _now;

        public SystemDate(DateTime now)
        {
            _now = now;
        }
        public DateTime Today => _now;

        public static SystemDate Now() => new SystemDate(DateTime.Now);
    }
}
