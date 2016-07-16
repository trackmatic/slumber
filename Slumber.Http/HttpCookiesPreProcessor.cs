namespace Slumber.Http
{
    public class HttpCookiesPreProcessor : IHttpPreProcessor
    {
        public void Process(IHttp http, IRequest request)
        {
            foreach (var cookie in http.GetCookies())
            {
                request.Add(cookie);
            }
        }
    }
}
