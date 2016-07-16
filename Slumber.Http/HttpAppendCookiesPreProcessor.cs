namespace Slumber.Http
{
    public class HttpAppendCookiesPreProcessor : IHttpPreProcessor
    {
        public void Process(IHttp http, IRestRequest request)
        {
            foreach (var cookie in http.GetCookies())
            {
                request.Add(cookie);
            }
        }
    }
}
