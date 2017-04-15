using System;
using Slumber.Http;
using Slumber.Json;
using Slumber.Xml;

namespace Slumber
{
    public static class SlumberConfigurationFactory
    {
        public static ISlumberConfiguration Default(string baseUri,
            string contentType = ContentTypes.ApplicationJson,
            TimeSpan? timeout = null,
            TimeSpan? connectTimeout = null,
            Action<ISlumberConfiguration> configure = null)
        {
            var configuration = new SlumberConfiguration(baseUri, timeout.GetValueOrDefault(TimeSpan.FromMinutes(1)), connectTimeout.GetValueOrDefault(TimeSpan.FromSeconds(5)));
            configuration.UseJsonSerialization()
                .UseXmlSerialization()
                .UseHttp(http => http.SetDefaultContentType(contentType));
            configure?.Invoke(configuration);
            return configuration;
        }

        public static ISlumberConfiguration Empty(string baseUri, TimeSpan timeout, TimeSpan connectTimeout, Action<ISlumberConfiguration> configure)
        {
            var configuration = new SlumberConfiguration(baseUri, timeout, connectTimeout);
            configure(configuration);
            return configuration;
        }
    }
}
