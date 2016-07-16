using System.Collections.Generic;
using System.Linq;

namespace Slumber
{
    public class Response<T> : IResponse<T>
    {
        private readonly List<HttpHeader> _headers;

        private readonly IDeserializer _deserializer;

        public Response(IDeserializer deserializer)
        {
            _headers = new List<HttpHeader>();
            _deserializer = deserializer;
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

        public bool HasError => StatusCode == -1 || StatusCode >= 400;

        public T Data
        {
            get
            {
                if (HasError)
                {
                    throw Exception;
                }

                if (string.IsNullOrEmpty(Content))
                {
                    throw new SlumberException("The response does not contain any content");
                }

                return _deserializer.Deserialize<T>(Content);
            }
        }

        public TErrorType GetErrorData<TErrorType>()
        {
            if (Exception == null)
            {
                throw new SlumberException("There is no exception to get content from");
            }

            return Exception.GetContent<TErrorType>(_deserializer);
        }
        
        public void SetException(SlumberException e)
        {
            Exception = e;
        }
    }
}