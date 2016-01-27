using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Slumber.Http
{
    public static class Extensions
    {
        private static readonly List<string> RestricedHeaders = new List<string>
        {
            "Accept",
            "Connection",
            "Content-Length",
            "Content-Type",
            "Date",
            "Except",
            "Host",
            "If-Modified-Since",
            "Range",
            "Referer",
            "Transfer-Encoding",
            "User-Agent",
            "Proxy-Connection"
        };

        public static void AppendHeader(this HttpWebRequest request, HttpHeader header)
        {
            if (RestricedHeaders.Contains(header.Name))
            {
                return;
            }

            request.Headers.Add(header.Name, header.Value);
        }

        public static void AppendHeaders(this HttpWebRequest request, IEnumerable<HttpHeader> headers)
        {
            foreach (var header in headers)
            {
                request.AppendHeader(header);
            }
        }

        public static void AppendCookie(this HttpWebRequest request, HttpCookie cookie)
        {
            request.CookieContainer.Add(new Cookie(cookie.GetName(), cookie.GetValue(), cookie.GetPath(), cookie.GetDomain()));
        }

        public static void AppendCookies(this HttpWebRequest request, IEnumerable<HttpCookie> cookies)
        {
            foreach (var cookie in cookies)
            {
                request.AppendCookie(cookie);
            }
        }

        public static RestResponse<T> CreateResponse<T>(this WebResponse webResponse, IDeserializer deserializer)
        {
            var http = new RestResponse<T>(deserializer);
            try
            {
                foreach (var name in webResponse.Headers.AllKeys)
                {
                    http.Headers.Add(new HttpHeader(name, webResponse.Headers[name]));
                }
                using (var stream = webResponse.GetResponseStream())
                {
                    if (stream == null)
                    {
                        throw new InvalidOperationException("Cannot read from an empty stream");
                    }

                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        var httpWebResponse = (HttpWebResponse)webResponse;
                        http.Content = reader.ReadToEnd();
                        http.StatusCode = (int)httpWebResponse.StatusCode;
                        if (http.HasError)
                        {
                            http.SetException(new UpstreamException(http.Content));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                http.SetException(new SlumberException(e));
            }
            return http;
        }
    }
}
