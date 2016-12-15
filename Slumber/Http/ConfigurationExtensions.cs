using System;

namespace Slumber.Http
{
    public static class ConfigurationExtensions
    {
        public static ISlumberConfiguration UseHttp(this ISlumberConfiguration configuration, Action<Http> configure = null)
        {
            configuration.UriEncoder = new DefaultUriEncoder(new HttpParameterEncoder());
            var http = new Http(configuration);
            configure?.Invoke(http);
            configuration.Http = http;
            return configuration;
        }
    }
}
