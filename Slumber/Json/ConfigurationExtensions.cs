using System;
using Newtonsoft.Json;

namespace Slumber.Json
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
