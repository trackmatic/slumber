using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Slumber.Http
{
    internal class ErrorHandler
    {
        private readonly ISerializationProvider _deserializer;

        public ErrorHandler(ISerializationProvider deserializer)
        {
            _deserializer = deserializer;
        }

        public Response<T> Handle<T>(Exception e)
        {
            try
            {
                return TryHandleWebException<T>(e) ?? NewResponseFromException<T>(e);
            }
            catch (Exception ex)
            {
                return NewResponseFromException<T>(ex);
            }
        }

        private Response<T> TryHandleWebException<T>(Exception e)
        {
            var we = TryExtractWebException(e);
            return we == null ? null : NewResponseFromWebException<T>(we);
        }

        private WebException TryExtractWebException(Exception e)
        {
            return e as WebException ?? TryExtractWebException(e as AggregateException);
        }

        private WebException TryExtractWebException(AggregateException e)
        {
            return e?.InnerExceptions.Where(x => x is WebException).Cast<WebException>().FirstOrDefault();
        }

        private Response<T> NewResponseFromWebException<T>(WebException e)
        {
            if (e.Response == null)
            {
                return NewResponseFromException<T>(e);
            }

            using (var stream = e.Response.GetResponseStream())
            {
                if (stream == null)
                {
                    return NewResponseFromException<T>(e);
                }


                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();

                    var http = new Response<T>(_deserializer)
                    {
                        StatusCode = (int)((HttpWebResponse)e.Response).StatusCode
                    };

                    foreach (var name in e.Response.Headers.AllKeys)
                    {
                        http.Headers.Add(new HttpHeader(name, e.Response.Headers[name]));
                    }

                    if (string.IsNullOrEmpty(content))
                    {
                        http.SetException(new SlumberException(e));
                    }
                    else
                    {
                        http.SetException(new SlumberUpstreamException(content));
                    }
                    return http;
                }
            }
        }

        private Response<T> NewResponseFromException<T>(Exception e)
        {
            var http = new Response<T>(_deserializer);
            http.SetException(new SlumberException(e));
            return http;
        }
    }
}
