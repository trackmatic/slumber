using System;

namespace Slumber.Http
{
    public class HttpHeadersPreProcessor : IHttpPreProcessor
    {
        public void OnExecuting(IHttp http, IRequest request)
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