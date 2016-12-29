using System;
using System.Net;
using System.Text;
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
            var webRequest = CreateWebRequest(request);
            try
            {
                var stream = await webRequest.GetRequestStreamAsync().ConfigureAwait(false);
                if (request.Data != null && request.ContainsHeader(Slumber.HttpHeaders.ContentType))
                {
                    var json = _configuration.Serialization.CreateSerializer(request).Serialize(request);
                    var uri = _configuration.UriEncoder.Encode(request);
                    _configuration.Log.Debug(@"POST {0}\r\n\r\n{1}", uri, json);
                    var buffer = Encoding.UTF8.GetBytes(json);
                    await stream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
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