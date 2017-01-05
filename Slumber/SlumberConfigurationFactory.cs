using System;
using Slumber.Http;
using Slumber.Json;
using Slumber.Xml;

namespace Slumber
{
    public static class SlumberConfigurationFactory
    {
        public static ISlumberConfiguration Default(string baseUri, string contentType = ContentTypes.ApplicationJson, TimeSpan? timeout = null, Action < ISlumberConfiguration> configure = null)
        {
            var configuration = new SlumberConfiguration(baseUri, timeout.GetValueOrDefault(TimeSpan.FromMinutes(1)));
            configuration.UseJsonSerialization().UseXmlSerialization().UseHttp(http => http.SetDefaultContentType(contentType));
            configure?.Invoke(configuration);
            return configuration;
        }

        public static ISlumberConfiguration Empty(string baseUri, TimeSpan timeout, Action<ISlumberConfiguration> configure)
        {
            var configuration = new SlumberConfiguration(baseUri, timeout);
            configure(configuration);
            return configuration;
        }
    }
}
