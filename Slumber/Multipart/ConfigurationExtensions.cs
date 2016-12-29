namespace Slumber.Multipart
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseJsonSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization.Register(new MultipartSerializerFactory());
            return configuration;
        }
    }
}
