using System;
using Newtonsoft.Json;

namespace Slumber.Json
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseMultipartSerialization(this ISlumberConfiguration configuration, Action<JsonSerializerSettings> customise = null)
        {
            configuration.Serialization.Register(new JsonSerializerFactory(customise));
            return configuration;
        }
    }
}
