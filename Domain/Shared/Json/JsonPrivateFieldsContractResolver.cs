using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shared.Json
{
    public class JsonPrivateFieldsContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (IsTupleType((type)))
            {
                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            }

            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Select(p => base.CreateProperty(p, memberSerialization))
                        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                   .Select(f => base.CreateProperty(f, memberSerialization)))
                        .ToList();
            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }

        public static bool IsTupleType(Type type, bool checkBaseTypes = false)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type == typeof(Tuple))
                return true;

            while (type != null)
            {
                if (type.IsGenericType)
                {
                    var genType = type.GetGenericTypeDefinition();
                    if (genType == typeof(Tuple<>)
                        || genType == typeof(Tuple<,>)
                        || genType == typeof(Tuple<,,>)
                        || genType == typeof(Tuple<,,,>)
                        || genType == typeof(Tuple<,,,,>)
                        || genType == typeof(Tuple<,,,,,>)
                        || genType == typeof(Tuple<,,,,,,>)
                        || genType == typeof(Tuple<,,,,,,,>))
                        return true;
                }

                if (!checkBaseTypes)
                    break;

                type = type.BaseType;
            }

            return false;
        }
    }



}
