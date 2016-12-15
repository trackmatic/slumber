using System.Collections.Generic;

namespace Slumber.Http
{
    public class HttpCookies
    {
        private readonly List<HttpCookie> _cookies;

        public HttpCookies()
        {
            _cookies = new List<HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            if (_cookies.Contains(cookie))
            {
                return;
            }
            _cookies.Add(cookie);
        }

        public IEnumerable<HttpCookie> Cookies => _cookies;
    }
}