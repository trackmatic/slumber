using System;
using System.Net;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public class HttpPost : IHttpMethod
    {
        private readonly ISlumberConfiguration _configuration;

        public HttpPost(ISlumberConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IResponse<T>> Execute<T>(IRequest request)
        {
            try
            {
                var serializer = CreateSerializer(request);
                var webRequest = CreateWebRequest(request);
                var stream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);
                var uri = _configuration.UriEncoder.Encode(request);
                _configuration.Log.Debug(@"POST {0}", uri);
                if (serializer != null)
                {
                    await serializer.Serialize(stream, request).ConfigureAwait(false);
                }
                var webResponse = await webRequest.GetResponseAsync().ConfigureAwait(false);
                return webResponse.CreateResponse<T>(_configuration);
            }
            catch (Exception e)
            {
                var handler = new ErrorHandler(_configuration.Serialization);
                return handler.Handle<T>(e);
            }
        }

        private ISerializer CreateSerializer(IRequest request)
        {
            if (request.Data == null || !request.ContainsHeader(Slumber.HttpHeaders.ContentType))
            {
                return null;
            }
            var serializer = _configuration.Serialization.CreateSerializer(request);
            return serializer;
        }

        private WebRequest CreateWebRequest(IRequest request)
        {
            var uri = _configuration.UriEncoder.Encode(request);
            var webRequest = WebRequest.Create(_configuration.BaseUri.Trim('/') + uri);
            webRequest.Method = request.Method;
            if (request.Data != null)
            {
                var contentType = request.GetHeader(Slumber.HttpHeaders.ContentType);
                if (contentType == null)
                {
                    throw new SlumberException("Content-Type header is required when data is present");
                }
                webRequest.ContentType = contentType.Value;
            }
            if (_configuration.Timeout > TimeSpan.Zero)
            {
                webRequest.Timeout = (int) _configuration.Timeout.TotalMilliseconds;
            }
            webRequest.PreAuthenticate = true;
            var httpWebRequest = (HttpWebRequest)webRequest;
            httpWebRequest.CookieContainer = httpWebRequest.CookieContainer ?? new CookieContainer();
            httpWebRequest.AppendCookies(request.Cookies);
            httpWebRequest.AppendHeaders(request.Headers);
            return webRequest;
        }
    }
}