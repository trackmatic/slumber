using System;
using System.Collections.Generic;
using System.Linq;

namespace Slumber
{
    public class RestResponse<T> : IRestResponse<T>
    {
        private readonly List<HttpHeader> _headers;

        public RestResponse()
        {
            _headers = new List<HttpHeader>();
            StatusCode = -1;
        }

        public int StatusCode { get; set; }

        public string Content { get; set; }

        public IList<HttpHeader> Headers => _headers;

        public Exception Exception { get; private set; }

        public HttpHeader GetHeader(string name)
        {
            return _headers.Single(x => x.Name == name);
        }

        public bool HasError => StatusCode == -1 || StatusCode >= 400;

        public T Data { get; set; }

        public void SetException(Exception e)
        {
            Exception = e;
        }
    }
}