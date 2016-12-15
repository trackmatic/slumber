namespace Slumber.Xml
{
    public class XmlSerializerFactory : ISerializationFactory
    {
        public ISerializer CreateSerializer()
        {
            return new HttpXmlSerializer();
        }

        public IDeserializer CreateDeserializer()
        {
            return new HttpXmlDeserializer();
        }
    }
}
