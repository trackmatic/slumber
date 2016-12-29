using System;

namespace Slumber.Multipart
{
    public class MultipartSerializerFactory : ISerializationFactory
    {
        public IDeserializer CreateDeserializer()
        {
            throw new NotImplementedException();
        }

        public ISerializer CreateSerializer()
        {
            return new MultipartSerializer();
        }

        public bool AppliesTo(string contentType)
        {
            return contentType.Contains("multipart/form-data");
        }
    }
}