using System.Threading.Tasks;

namespace Slumber
{
    public class SlumberClient : ISlumberClient
    {
        private readonly ISlumberConfiguration _configuration;
        
        public SlumberClient(ISlumberConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<IResponse<T>> ExecuteAsync<T>(IRequest<T> request)
        {
            _configuration.Validate();
            return _configuration.Http.Execute<T>(request);
        }

        public ISlumberConfiguration Configuration => _configuration;
    }
}
