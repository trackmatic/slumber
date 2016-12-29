namespace Slumber.Xml
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseXmlSerialization(this ISlumberConfiguration configuration)
        {
            configuration.Serialization.Register(new XmlSerializerFactory());
            return configuration;
        }
    }
}
