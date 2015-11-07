using System.Collections.Generic;
using System.Linq;

namespace Slumber.Http
{
    public class HttpCookies
    {
        private readonly List<HttpCookie> _cookies;

        public HttpCookies()
        {
            _cookies = new List<HttpCookie>();
        }

        public void Register(HttpCookie cookie)
        {
            if (_cookies.Contains(cookie))
            {
                return;
            }
            _cookies.Add(cookie);
        }

        public void Register(IRestResponse response)
        {
            foreach (var header in response.Headers.Where(x => x.Name == Slumber.HttpHeaders.SetCookie))
            {
                var cookie = new HttpCookie(header.Value);
                Register(cookie);
            }
        }

        public void Append(IRestRequest request)
        {
            foreach (var cookie in _cookies)
            {
                request.Add(cookie);
            }
        }
    }
}