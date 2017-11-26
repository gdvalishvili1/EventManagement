using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class Command
    {
        public abstract CommandExecutionResult Execute();
    }
}
