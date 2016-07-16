using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public class HttpPost : IHttpMethod
    {
        private readonly ISlumberConfiguration _configuration;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public HttpPost(ISlumberConfiguration configuration)
        {
            _configuration = configuration;
            _serializer = configuration.Serialization.CreateSerializer();
            _deserializer = configuration.Serialization.CreateDeserializer();
        }

        public async Task<IResponse<T>> Execute<T>(IRequest request)
        {
            var webRequest = CreateWebRequest(request);
            try
            {
                var stream = await webRequest.GetRequestStreamAsync();
                WriteToStream(stream, request);
                var webResponse = await webRequest.GetResponseAsync();
                return webResponse.CreateResponse<T>(_deserializer);
            }
            catch (Exception e)
            {
                var handler = new ErrorHandler(_deserializer);
                return handler.Handle<T>(e);
            }
        }

        private void WriteToStream(Stream stream, IRequest request)
        {
            var uri = _configuration.UriEncoder.Encode(request);
            using (var writer = new StreamWriter(stream))
            {
                var json = _serializer.Serialize(request.Data);
                _configuration.Log.Debug(@"POST {0}\r\n\r\n{1}", uri, json);
                writer.Write(json);
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