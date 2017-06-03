using System.Net;

namespace Slumber.Http
{
    public static class HttpWebRequestFactory
    {
        public static HttpWebRequest CreateHttpWebRequest(IRequest request, string uri, ISlumberConfiguration configuration)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(configuration.BaseUri.Trim('/') + uri);
            httpWebRequest.Timeout = (int)configuration.Timeout.TotalMilliseconds;
            httpWebRequest.CookieContainer = httpWebRequest.CookieContainer ?? new CookieContainer();
            httpWebRequest.AppendCookies(request.Cookies);
            httpWebRequest.AppendHeaders(request.Headers);
            httpWebRequest.Method = request.Method;
            httpWebRequest.UserAgent = configuration.UserAgent?.Invoke();
            return httpWebRequest;
        }
    }
}
