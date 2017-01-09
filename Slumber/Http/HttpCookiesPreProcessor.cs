namespace Slumber.Http
{
    internal class HttpCookiesPreProcessor : IHttpPreProcessor
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
