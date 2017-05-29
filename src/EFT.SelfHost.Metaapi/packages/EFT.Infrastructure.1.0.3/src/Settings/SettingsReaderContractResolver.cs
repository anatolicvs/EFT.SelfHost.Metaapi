using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace EFT.Infrastructure.Settings
{
    public class SettingsReaderContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(p => CreateProperty(p, memberSerialization))
                .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                     .Select(f => CreateProperty(f, memberSerialization)))
                     .ToList();

            props.ForEach(p =>
            {
                p.Writable = true;
                p.Readable = true;
            });
            return props;
        }
    }
}
