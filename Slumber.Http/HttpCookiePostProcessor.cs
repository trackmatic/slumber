﻿using System.Linq;

namespace Slumber.Http
{
    public class HttpCookiePostProcessor : IHttpPostProcessor
    {
        public void Process(IHttp http, IRestRequest request, IRestResponse response)
        {
            foreach (var header in response.Headers.Where(x => x.Name == Slumber.HttpHeaders.SetCookie))
            {
                var cookie = new HttpCookie(header.Value);
                http.Add(cookie);
            }
        }
    }
}
