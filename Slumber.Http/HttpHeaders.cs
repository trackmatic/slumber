using System.Collections.Generic;

namespace Slumber.Http
{
    public class HttpHeaders
    {
        private readonly Dictionary<string, List<HttpHeader>> _headers;

        public HttpHeaders()
        {
            _headers = new Dictionary<string, List<HttpHeader>>();
        }

        public void Register(string method, HttpHeader header)
        {
            if (!_headers.ContainsKey(method))
            {
                _headers.Add(method, new List<HttpHeader>());
            }
            _headers[method].Add(header);
        }

        public void Append(IRestRequest request)
        {
            if (!_headers.ContainsKey(request.Method))
            {
                return;
            }

            foreach (var header in _headers[request.Method])
            {
                if (request.Contains(header))
                {
                    continue;
                }

                request.Add(header);
            }
        }
    }
}
