using System.Threading.Tasks;

namespace Slumber
{
    /// <summary>
    /// Abstraction of a Slumber Client
    /// </summary>
    public interface ISlumberClient
    {
        /// <summary>
        /// Executes the request asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResponse<T>> ExecuteAsync<T>(IRequest<T> request);

        /// <summary>
        /// Exposes configuration
        /// </summary>
        ISlumberConfiguration Configuration { get; }
    }
}
