using System;
using Newtonsoft.Json;

namespace Slumber.Json
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseJsonSerialization(this ISlumberConfiguration configuration, Action<JsonSerializerSettings> customise = null)
        {
            configuration.Serialization.Register(ContentTypes.ApplicationJson, new JsonSerializerFactory(configuration.Log, customise));
            return configuration;
        }
    }
}
