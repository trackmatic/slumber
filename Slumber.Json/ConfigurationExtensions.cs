using Slumber;

namespace Slmber.Json
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseJsonSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization = new JsonSerializerFactory();
            return configuration;
        }
    }
}
