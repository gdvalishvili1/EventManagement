using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Persistence
{
    public class PersitedObjectContainer
    {
        public PersitedObjectContainer(Guid id, string data, int version)
        {
            Id = id;
            Data = data;
            Version = version;
        }
        public Guid Id { get; }
        public string Data { get; }
        public int Version { get; }
    }
}
