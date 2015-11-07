using System.Threading.Tasks;

namespace Slumber
{
    /// <summary>
    /// Abstraction of a Nap Client
    /// </summary>
    public interface ISlumberClient
    {
        /// <summary>
        /// Executes the request synchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        IRestResponse<T> Execute<T>(IRestRequest<T> request);

        /// <summary>
        /// Executes the request asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest<T> request);
    }
}
