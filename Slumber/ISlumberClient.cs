using System.Threading.Tasks;

namespace Slumber
{
    /// <summary>
    /// Abstraction of a Nap Client
    /// </summary>
    public interface ISlumberClient
    {
        /// <summary>
        /// Executes the request asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest<T> request);
    }
}
