using System.Collections.Generic;
using System.Linq;

namespace Slumber
{
    public class SerializationProvider : ISerializationProvider
    {
        private readonly string ContentTypeHeader = HttpHeaders.ContentType;

        private readonly List<ISerializationFactory> _factories;

        public SerializationProvider()
        {
            _factories = new List<ISerializationFactory>();
        }

        public void Register(ISerializationFactory factory)
        {
            _factories.Add(factory);
        }
        
        public ISerializer CreateSerializer(IRequest request)
        {
            var factory = GetFactory(request);
            return factory.CreateSerializer(request);
        }

        public IDeserializer CreateDeserializer(IResponse response)
        {
            var factory = GetFactory(response);
            return factory.CreateDeserializer();
        }

        private ISerializationFactory GetFactory(IHeaders headers)
        {
            var contentType = GetContentType(headers);
            var factories = _factories.Where(x => x.AppliesTo(contentType)).ToList();
            if (!factories.Any())
            {
                throw new SlumberException($"A serialization factory could not be found for content-type {contentType}");
            }
            if (factories.Count > 1)
            {
                throw new SlumberException($"Multiple serializaction factories were found for content-type {contentType}");
            }
            return factories[0];
        }

        private string GetContentType(IHeaders headers)
        {
            EnsureContentTypeExists(headers);
            return headers.GetHeader(ContentTypeHeader).Value;
        }

        private void EnsureContentTypeExists(IHeaders headers)
        {
            if (headers.ContainsHeader(ContentTypeHeader))
            {
                return;
            }

            throw new SlumberException("A serializer cannot be retrieved if a content-type header is not present");
        }
    }
}
