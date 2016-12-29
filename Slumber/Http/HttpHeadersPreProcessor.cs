namespace Slumber.Http
{
    public class HttpHeadersPreProcessor : IHttpPreProcessor
    {
        public void OnExecuting(IHttp http, IRequest request)
        {
            foreach (var header in http.GetHeaders(request.Method))
            {
                if (request.ContainsHeader(header.Name))
                {
                    continue;
                }

                request.Add(header);
            }
        }
    }
}