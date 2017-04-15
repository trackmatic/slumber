using System;
using System.Net;
using System.Threading.Tasks;

namespace Slumber.Http
{
    internal class HttpPost : IHttpMethod
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
                
                var timeoutHandler = new Func<Exception>(() =>
                {
                    webRequest.Abort();
                    return new HttpTimeoutException($"Host did not respond within {_configuration.ConnectTimeout}, the connection has been aborted");
                });

                var stream = await webRequest.GetRequestStreamAsync().WithTimeout(_configuration.ConnectTimeout, timeoutHandler).ConfigureAwait(false);
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
            var httpWebRequest = HttpWebRequestFactory.CreateHttpWebRequest(request, uri, _configuration);
            EnsureContentTypeHasBeenProvidedWhenRequired(request, httpWebRequest);
            httpWebRequest.PreAuthenticate = true;
            return httpWebRequest;
        }

        private void EnsureContentTypeHasBeenProvidedWhenRequired(IRequest request, HttpWebRequest httpWebRequest)
        {
            if (request.Data == null)
            {
                return;
            }

            var contentType = request.GetHeader(Slumber.HttpHeaders.ContentType);
            if (contentType == null)
            {
                throw new SlumberException("Content-Type header is required when data is present");
            }

            httpWebRequest.ContentType = contentType.Value;
        }
    }
}