using System;
using System.Net;
using System.Threading.Tasks;

namespace Slumber.Http
{
    internal class HttpGet : IHttpMethod
    {
        private readonly ISlumberConfiguration _configuration;

        public HttpGet(ISlumberConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IResponse<T>> Execute<T>(IRequest request)
        {
            var uri = _configuration.UriEncoder.Encode(request);
            var webRequest = CreateWebRequest(request, uri);
            _configuration.Log.Debug(@"GET {0}", uri);
            try
            {
                var timeoutHandler = new Func<Exception>(() =>
                {
                    webRequest.Abort();
                    return new HttpTimeoutException($"Host did not respond within {_configuration.ConnectTimeout}, the connection has been aborted");
                });

                var webResponse = await webRequest.GetResponseAsync().WithTimeout(_configuration.ConnectTimeout, timeoutHandler).ConfigureAwait(false);
                return webResponse.CreateResponse<T>(_configuration);
            }
            catch (Exception e)
            {
                var handler = new ErrorHandler(_configuration.Serialization);
                return handler.Handle<T>(e);
            }
        }

        protected virtual HttpWebRequest CreateWebRequest(IRequest request, string uri)
        {
            var accept = request.GetHeader(Slumber.HttpHeaders.Accept);
            if (accept == null)
            {
                throw new InvalidOperationException("Accept header is required for a GET request");
            }
            var httpWebRequest = HttpWebRequestFactory.CreateHttpWebRequest(request, uri, _configuration);
            httpWebRequest.Accept = accept.Value;
            return httpWebRequest;
        }
    }
}
