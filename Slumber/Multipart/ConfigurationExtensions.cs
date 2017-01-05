namespace Slumber.Multipart
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseMultipartSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization.Register(ContentTypes.MultipartFormData, new MultipartSerializerFactory());
            return configuration;
        }
    }
}
