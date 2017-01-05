namespace Slumber.Xml
{
    public class XmlSerializerFactory : ISerializationFactory
    {
        public ISerializer CreateSerializer(IRequest request)
        {
            return new HttpXmlSerializer();
        }

        public IDeserializer CreateDeserializer()
        {
            return new HttpXmlDeserializer();
        }
    }
}
