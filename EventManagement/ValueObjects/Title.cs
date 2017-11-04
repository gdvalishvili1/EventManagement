using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public abstract class Title
    {
        public Title(string value)
        {
            Value = value;
        }
        public string Value { get; }
    }
}
