using System;
using System.Threading.Tasks;

namespace Slumber
{
    public class SlumberClient : ISlumberClient
    {
        private readonly ISlumberConfiguration _configuration;

        public SlumberClient(string baseUri, Action<ISlumberConfiguration> configure) : this(baseUri, TimeSpan.FromMinutes(1), configure)
        {
            
        }

        public SlumberClient(string baseUri, TimeSpan timeout, Action<ISlumberConfiguration> configure)
        {
            _configuration = new SlumberConfiguration(baseUri, timeout);
            configure(_configuration);
        }

        public Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest<T> request)
        {
            _configuration.Validate();
            return _configuration.Http.Execute<T>(request);
        }
    }
}
