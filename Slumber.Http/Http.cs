using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public class Http : IHttp
    {
        private readonly IDictionary<string, IHttpMethod> _methods;

        private readonly HttpCookies _cookies;

        private readonly HttpHeaders _headers;

        public Http(ISlumberConfiguration configuration) : this(Methods(configuration))
        {
            
        }

        public Http(IDictionary<string, IHttpMethod> methods)
        {
            _methods = methods;
            _headers = new HttpHeaders();
            _cookies = new HttpCookies();
        }

        public Task<IRestResponse<T>> Execute<T>(IRestRequest request)
        {
            if (!_methods.ContainsKey(request.Method))
            {
                throw new InvalidOperationException("Http Method Not Supported");
            }
            _headers.Append(request);
            _cookies.Append(request);
            var method = _methods[request.Method];
            var task = method.Execute<T>(request).ContinueWith(x =>
            {
                var response = x.Result;
                _cookies.Register(response);
                return response;
            });
            return task;
        }

        #region Configuration

        public Http Add(HttpCookie cookie)
        {
            _cookies.Register(cookie);
            return this;
        }

        public Http Add(string method, HttpHeader header)
        {
            _headers.Register(method, header);
            return this;
        }

        public Http UseJsonAsDefaultContentType()
        {
            SetDefaultContentType("application/json");
            return this;
        }

        public Http UseXmlAsDefaultContentType()
        {
            SetDefaultContentType("application/xml");
            return this;
        }

        public Http SetDefaultContentType(string type)
        {
            _headers.Register(HttpMethods.Get, HttpHeader.Accept(type));
            _headers.Register(HttpMethods.Post, HttpHeader.ContentType(type));
            _headers.Register(HttpMethods.Put, HttpHeader.ContentType(type));
            _headers.Register(HttpMethods.Delete, HttpHeader.ContentType(type));
            _headers.Register(HttpMethods.Patch, HttpHeader.ContentType(type));
            return this;
        }

        #endregion

        #region Factory Methods

        private static IDictionary<string, IHttpMethod> Methods(ISlumberConfiguration configuration)
        {
            return new Dictionary<string, IHttpMethod>
            {
                {HttpMethods.Get, new HttpGet(configuration)},
                {HttpMethods.Post, new HttpPost(configuration)},
                {HttpMethods.Put, new HttpPost(configuration)},
                {HttpMethods.Delete, new HttpPost(configuration)},
                {HttpMethods.Patch, new HttpPost(configuration)}
            };
        }

        #endregion
    }
}