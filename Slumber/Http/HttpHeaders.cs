using System.Collections.Generic;
using System.Linq;

namespace Slumber.Http
{
    internal class HttpHeaders
    {
        private static readonly List<HttpHeader> Empty = new List<HttpHeader>();
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
            ClearPreviousHeaders(method, header);
            _headers[method].Add(header);
        }

        public IEnumerable<HttpHeader> this[string method]
        {
            get
            {
                if (!_headers.ContainsKey(method))
                {
                    return Empty;
                }
                return _headers[method];
            }
        }

        private void ClearPreviousHeaders(string method, HttpHeader header)
        {
            foreach (var previous in _headers[method].Where(x => x.Equals(header)).ToList())
            {
                _headers[method].Remove(previous);
            }
        }
    }
}
