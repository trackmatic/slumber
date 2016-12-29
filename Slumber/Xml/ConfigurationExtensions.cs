namespace Slumber.Xml
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration ConfigureWithXmlSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization.Register(new XmlSerializerFactory());
            return configuration;
        }
    }
}
