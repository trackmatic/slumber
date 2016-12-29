using System.Collections.Generic;
using System.Linq;

namespace Slumber
{
    public class Response<T> : IResponse<T>
    {
        private readonly List<HttpHeader> _headers;
        private readonly ISerializationProvider _serialization;

        public Response(ISerializationProvider serialization)
        {
            _headers = new List<HttpHeader>();
            _serialization = serialization;
            StatusCode = -1;
        }

        public int StatusCode { get; set; }

        public string Content { get; set; }

        public IList<HttpHeader> Headers => _headers;

        public SlumberException Exception { get; private set; }

        public HttpHeader GetHeader(string name)
        {
            return _headers.Single(x => x.Name == name);
        }

        public bool ContainsHeader(string name)
        {
            return _headers.Any(x => x.Name == name);
        }

        public bool HasError => Exception != null;

        public T Data => GetContentData<T>();

        public TContentType GetContentData<TContentType>()
        {
            if (HasError)
            {
                throw Exception;
            }
            
            if (string.IsNullOrEmpty(Content))
            {
                throw new SlumberException("The response does not contain any content");
            }

            return _serialization.CreateDeserializer(this).Deserialize<TContentType>(Content);
        }

        public TErrorType GetErrorData<TErrorType>()
        {
            if (Exception == null)
            {
                throw new SlumberException("There is no exception to get content from");
            }

            return Exception.GetContent<TErrorType>(_serialization.CreateDeserializer(this));
        }
        
        public void SetException(SlumberException e)
        {
            Exception = e;
        }
    }
}