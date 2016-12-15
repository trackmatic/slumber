using System.Threading.Tasks;

namespace Slumber.Http
{
    public interface IHttpMethod
    {
        Task<IResponse<T>> Execute<T>(IRequest request);
    }
}
