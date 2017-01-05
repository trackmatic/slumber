using System.Linq;
using System.Collections.Generic;

namespace Slumber
{
    public class SerializationProvider : ISerializationProvider
    {
        private readonly string ContentTypeHeader = HttpHeaders.ContentType;
        private readonly Dictionary<string, ISerializationFactory> _factories;

        public SerializationProvider()
        {
            _factories = new Dictionary<string, ISerializationFactory>();
        }

        public void Register(string contentType, ISerializationFactory factory)
        {
            if (_factories.ContainsKey(contentType))
            {
                throw new SlumberException($"Multiple serializaction factories were found for content-type {contentType}");
            }
            _factories.Add(contentType, factory);
        }

        public void Remove(string contentType)
        {
            var key = EnsureFactoryExistsForContentType(contentType);
            _factories.Remove(key);
        }

        public ISerializationFactory GetFactory(string contentType)
        {
            var key = EnsureFactoryExistsForContentType(contentType);
            return _factories[key];
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
            var factory = GetFactory(contentType);
            return factory;
        }

        private string GetContentType(IHeaders headers)
        {
            EnsureContentTypeHeadersExists(headers);
            return headers.GetHeader(ContentTypeHeader).Value;
        }

        private void EnsureContentTypeHeadersExists(IHeaders headers)
        {
            if (headers.ContainsHeader(ContentTypeHeader))
            {
                return;
            }
            throw new SlumberException("A serializer cannot be retrieved if a content-type header is not present");
        }

        private string EnsureFactoryExistsForContentType(string contentType)
        {
            var key = _factories.Keys.Where(contentType.Contains).FirstOrDefault();
            if (key != null)
            {
                return key;
            }
            throw new SlumberException($"A serialization factory could not be found for content-type {contentType}");
        }
    }
}
