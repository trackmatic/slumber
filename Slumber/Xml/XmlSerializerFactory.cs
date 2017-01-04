namespace Slumber.Xml
{
    public class XmlSerializerFactory : ISerializationFactory
    {
        public ISerializer CreateSerializer(IRequest request)
        {
            return new HttpXmlSerializer();
        }

        public bool AppliesTo(string contentType)
        {
            return contentType.Contains("application/xml");
        }

        public IDeserializer CreateDeserializer()
        {
            return new HttpXmlDeserializer();
        }
    }
}
