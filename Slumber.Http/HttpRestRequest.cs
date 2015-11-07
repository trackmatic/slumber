using System;
using System.Collections.Generic;

namespace Slumber.Http
{
    public class HttpRestRequest<T> : IRestRequest<T>
    {
        private readonly List<RestQueryParameter> _queryParameters;

        private readonly Dictionary<string, HttpHeader> _headers;

        private readonly List<HttpCookie> _cookies;

        public HttpRestRequest(string path, string method)
        {
            _queryParameters = new List<RestQueryParameter>();
            _headers = new Dictionary<string, HttpHeader>();
            _cookies = new List<HttpCookie>();
            Path = path;
            Method = method;
        }

        public IEnumerable<HttpCookie> Cookies => _cookies;

        public Type ResponseType => typeof (T);

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

        public IEnumerable<RestQueryParameter> Query => _queryParameters;

        public IEnumerable<HttpHeader> Headers => _headers.Values;

        public void AddQueryParameter(string name, object value, bool ignoreEmptyValues = false)
        {
            if (value == null && ignoreEmptyValues)
            {
                return;
            }
            var parameter = new RestQueryParameter
            {
                Name = name,
                Value = value
            };
            AddQueryParameter(parameter);
        }

        public void AddQueryParameter(RestQueryParameter parameter)
        {
            _queryParameters.Add(parameter);
        }

        public void AddHeader(string name, string value)
        {
            Add(new HttpHeader(name, value));
        }

        public void Add(HttpHeader header)
        {
            _headers.Add(header.Name, header);
        }

        public bool Contains(HttpHeader header)
        {
            return _headers.ContainsKey(header.Value);
        }

        public void Add(HttpCookie cookie)
        {
            _cookies.Add(cookie);
        }

        public HttpRestRequest<T> Customize(Action<HttpRestRequest<T>> configure)
        {
            configure(this);
            return this;
        }
    }
}