using System;
using System.IO;
using System.Net;

namespace Slumber.Http
{
    public class ErrorHandler
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
                return e is WebException ? NewResponseFromWebException<T>(e as WebException) : NewResponseFromException<T>(e);
            }
            catch (Exception ex)
            {
                return NewResponseFromException<T>(ex);
            }
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
                    if (string.IsNullOrEmpty(content))
                    {
                        http.SetException(new SlumberException(e));
                    }
                    else
                    {
                        http.SetException(new UpstreamException(content));
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
