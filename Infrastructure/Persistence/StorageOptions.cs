using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class StorageOptions
    {
        public string TableName { get; set; }
        public StorageOptions(string tableName)
        {
            TableName = tableName;
        }
    }
}
