using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ValueObjects
{
    public abstract class Title
    {
        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("title can not be null,empty or whitespaces", nameof(value));
            }

            Value = value;
        }
        public string Value { get; }
    }
}
