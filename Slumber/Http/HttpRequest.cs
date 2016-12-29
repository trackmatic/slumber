using System;
using System.Collections.Generic;
using System.Linq;

namespace Slumber.Http
{
    public class HttpRequest<T> : IRequest<T>
    {
        private readonly List<QueryParameter> _queryParameters;
        private readonly Dictionary<string, HttpHeader> _headers;
        private readonly List<HttpCookie> _cookies;

        public HttpRequest(string path, string method)
        {
            _queryParameters = new List<QueryParameter>();
            _headers = new Dictionary<string, HttpHeader>();
            _cookies = new List<HttpCookie>();
            Path = path;
            Method = method;
        }

        public IEnumerable<HttpCookie> Cookies => _cookies;
        
        public HttpHeader GetHeader(string name)
        {
            if (!_headers.ContainsKey(name))
            {
                return null;
            }
            return _headers[name];
        }

        public string Path { get; }

        public string Method { get; }

        public object Data { get; set; }

        public IEnumerable<QueryParameter> Query => _queryParameters;

        public IList<HttpHeader> Headers => _headers.Values.ToList();

        public void AddQueryParameter(string name, object value, bool ignoreEmptyValues = false)
        {
            if (value == null && ignoreEmptyValues)
            {
                return;
            }
            var parameter = new QueryParameter
            {
                Name = name,
                Value = value
            };
            AddQueryParameter(parameter);
        }

        public void AddQueryParameter(QueryParameter parameter)
        {
            _queryParameters.Add(parameter);
        }

        public void AddHeader(string name, string value)
        {
            Add(new HttpHeader(name, value));
        }

        public void RemoveHeader(string name)
        {
            if (!_headers.ContainsKey(name))
            {
                return;
            }
            _headers.Remove(name);
        }

        public void Add(HttpHeader header)
        {
            if (_headers.ContainsKey(header.Name))
            {
                return;
            }
            _headers.Add(header.Name, header);
        }

        public bool ContainsHeader(string name)
        {
            return _headers.ContainsKey(name);
        }
        
        public void Add(HttpCookie cookie)
        {
            _cookies.Add(cookie);
        }

        public HttpRequest<T> Customize(Action<HttpRequest<T>> configure)
        {
            configure(this);
            return this;
        }
    }
}