using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public class Http : IHttp
    {
        private readonly IDictionary<string, IHttpMethod> _methods;
        private readonly HttpCookies _cookies;
        private readonly HttpHeaders _headers;
        private readonly List<IHttpPostProcessor> _postProcessors;
        private readonly List<IHttpPreProcessor> _preProcessors;

        public Http(ISlumberConfiguration configuration) : this(Methods(configuration))
        {
            
        }

        public Http(IDictionary<string, IHttpMethod> methods)
        {
            _methods = methods;
            _headers = new HttpHeaders();
            _cookies = new HttpCookies();
            _postProcessors = new List<IHttpPostProcessor> {new HttpCookiePostProcessor()};
            _preProcessors = new List<IHttpPreProcessor>
            {
                new HttpAppendHeadersPreProcessor(),
                new HttpAppendCookiesPreProcessor()
            };
        }

        public Task<IRestResponse<T>> Execute<T>(IRestRequest request)
        {
            if (!_methods.ContainsKey(request.Method))
            {
                throw new SlumberException("Http method not supported");
            }
            OnBeforeProcess(request);
            var method = _methods[request.Method];
            var task = method.Execute<T>(request).ContinueWith(x =>
            {
                var response = x.Result;
                OnAfterProcess(request, response);
                return response;
            });
            return task;
        }

        private void OnBeforeProcess(IRestRequest request)
        {
            foreach (var processor in _preProcessors)
            {
                processor.Process(this, request);
            }
        }

        private void OnAfterProcess(IRestRequest request, IRestResponse response)
        {
            foreach (var processor in _postProcessors)
            {
                processor.Process(this, request, response);
            }
        }

        public IEnumerable<HttpCookie> GetCookies()
        {
            return _cookies.Cookies;
        }

        public IEnumerable<HttpHeader> GetHeaders(string method)
        {
            return _headers[method];
        }

        #region Configuration

        public IHttp Add(HttpCookie cookie)
        {
            _cookies.Add(cookie);
            return this;
        }

        public IHttp Add(string method, HttpHeader header)
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

        public Http Add(IHttpPostProcessor processor)
        {
            if (_postProcessors.Contains(processor))
            {
                return this;
            }
            _postProcessors.Add(processor);
            return this;
        }

        public Http Add(IHttpPreProcessor processor)
        {
            if (_preProcessors.Contains(processor))
            {
                return this;
            }
            _preProcessors.Add(processor);
            return this;
        }

        public Http Add(IHttpInterceptor interceptor)
        {
            Add((IHttpPreProcessor)interceptor);
            Add((IHttpPostProcessor) interceptor);
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