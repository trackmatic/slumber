using System;
using System.Net;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public class HttpGet : IHttpMethod
    {
        private readonly ISlumberConfiguration _configuration;
        private readonly IDeserializer _deserializer;

        public HttpGet(ISlumberConfiguration configuration)
        {
            _configuration = configuration;
            _deserializer = configuration.Serialization.CreateDeserializer();
        }

        public async Task<IResponse<T>> Execute<T>(IRequest request)
        {
            var uri = _configuration.UriEncoder.Encode(request);
            var webRequest = NewWebRequest(request, uri);
            _configuration.Log.Debug(@"GET {0}", uri);
            try
            {
                var webResponse = await webRequest.GetResponseAsync();
                return webResponse.CreateResponse<T>(_configuration, _deserializer);
            }
            catch (Exception e)
            {
                var handler = new ErrorHandler(_deserializer);
                return handler.Handle<T>(e);
            }
        }

        protected virtual HttpWebRequest NewWebRequest(IRequest request, string uri)
        {
            var webRequest = WebRequest.Create(_configuration.BaseUri.Trim('/') + uri);
            if (_configuration.Timeout > TimeSpan.Zero)
            {
                webRequest.Timeout = (int) _configuration.Timeout.TotalMilliseconds;
            }
            var httpWebRequest = (HttpWebRequest)webRequest;
            var accept = request.GetHeader(Slumber.HttpHeaders.Accept);
            if (accept == null)
            {
                throw new InvalidOperationException("Accept header is required for a GET request");
            }
            httpWebRequest.CookieContainer = httpWebRequest.CookieContainer ?? new CookieContainer();
            httpWebRequest.Accept = accept.Value;
            httpWebRequest.AppendCookies(request.Cookies);
            httpWebRequest.AppendHeaders(request.Headers);
            return httpWebRequest;
        }
    }
}
