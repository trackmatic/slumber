using System.Threading.Tasks;

namespace Slumber.Http
{
    public interface IHttpMethod
    {
        Task<IRestResponse<T>> Execute<T>(IRestRequest request);
    }
}
