using System;
using Newtonsoft.Json;
using Slumber;

namespace Slmber.Json
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseJsonSerialization(this ISlumberConfiguration configuration, Action<JsonSerializerSettings> customise = null)
        {
            configuration.Serialization = new JsonSerializerFactory(customise);
            return configuration;
        }
    }
}
