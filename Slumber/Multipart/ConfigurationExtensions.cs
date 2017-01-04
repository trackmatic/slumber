namespace Slumber.Multipart
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseMultipartSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization.Register(new MultipartSerializerFactory());
            return configuration;
        }
    }
}
