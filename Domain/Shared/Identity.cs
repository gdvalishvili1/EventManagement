using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class Identity : IEquatable<Identity>
    {
        public Guid AsGuid()
        {
            return Guid.Parse(Value);
        }
        public Identity()
        {
            this.Value = Guid.NewGuid().ToString();
        }

        [JsonConstructor]
        public Identity(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("not be null or empty", nameof(value));
            }

            this.Value = value;
        }

        public string Value { get; }

        public bool Equals(Identity id)
        {
            if (object.ReferenceEquals(this, id)) return true;
            if (object.ReferenceEquals(null, id)) return false;
            return this.Value.Equals(id.Value);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() + this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
