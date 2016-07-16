using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slumber
{
    /// <summary>
    /// An abstraction of the Http layer
    /// </summary>
    public interface IHttp
    {
        Task<IResponse<T>> Execute<T>(IRestRequest request);

        IHttp Add(HttpCookie cookie);

        IHttp Add(string method, HttpHeader header);

        IEnumerable<HttpCookie> GetCookies();

        IEnumerable<HttpHeader> GetHeaders(string method);
    }
}
