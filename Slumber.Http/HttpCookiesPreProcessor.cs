namespace Slumber.Http
{
    public class HttpCookiesPreProcessor : IHttpPreProcessor
    {
        public void OnExecuting(IHttp http, IRequest request)
        {
            foreach (var cookie in http.GetCookies())
            {
                request.Add(cookie);
            }
        }
    }
}
