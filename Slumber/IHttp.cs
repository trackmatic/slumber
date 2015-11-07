using System.Threading.Tasks;

namespace Slumber
{
    /// <summary>
    /// An abstraction of the Http layer
    /// </summary>
    public interface IHttp
    {
        Task<IRestResponse<T>> Execute<T>(IRestRequest request);
    }
}
