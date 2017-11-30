using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Json
{
    public class JsonParser<T>
    {
        public string AsJson(T obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateFieldsContractResolver(),
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
            };
            var payload = JsonConvert.SerializeObject(obj, settings);
            return payload;
        }

        public T FromJson(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            };
            var obj = JsonConvert.DeserializeObject<T>(json, settings);

            return obj;
        }
    }
}
