using System.Linq;

namespace Slumber.Http
{
    public class HttpCookiesPostProcessor : IHttpPostProcessor
    {
        public void OnExecuted(IHttp http, IRequest request, IResponse response)
        {
            foreach (var header in response.Headers.Where(x => x.Name == Slumber.HttpHeaders.SetCookie))
            {
                var cookie = new HttpCookie(header.Value);
                http.Add(cookie);
            }
        }
    }
}
