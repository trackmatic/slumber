using System;

namespace Slumber.Http
{
    public class HttpAppendHeadersPreProcessor : IHttpPreProcessor
    {
        public void Process(IHttp http, IRestRequest request)
        {
            foreach (var header in http.GetHeaders(request.Method))
            {
                if (request.Contains(header))
                {
                    continue;
                }

                request.Add(header);
            }
        }
    }
}