using System;

namespace Slumber.Multipart
{
    public class MultipartSerializerFactory : ISerializationFactory
    {
        public IDeserializer CreateDeserializer()
        {
            throw new NotImplementedException();
        }

        public ISerializer CreateSerializer(IRequest request)
        {
            return CreateSerializer(request, DateTime.Now);
        }

        public ISerializer CreateSerializer(IRequest request, DateTime now)
        {
            request.RemoveHeader(HttpHeaders.ContentType);
            var boundary = "---------------------------" + now.Ticks.ToString("x");
            var header = $"multipart/form-data; boundary={boundary}";
            request.AddHeader(HttpHeaders.ContentType, header);
            return new MultipartSerializer(boundary);
        }

        public bool AppliesTo(string contentType)
        {
            return contentType.Contains("multipart/form-data");
        }
    }
}