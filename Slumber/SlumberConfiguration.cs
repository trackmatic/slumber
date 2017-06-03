using System;

namespace Slumber
{
    public class SlumberConfiguration : ISlumberConfiguration
    {
        internal SlumberConfiguration(string baseUri, TimeSpan timeout, TimeSpan connectTimeout)
        {
            BaseUri = baseUri;
            Timeout = timeout;
            Log = new NullLogger();
            Serialization = new SerializationProvider();
            ConnectTimeout = connectTimeout;
            UserAgent = () => "Slumber/1.0";
        }

        public string BaseUri { get; }

        public TimeSpan Timeout { get; }
        public TimeSpan ConnectTimeout { get; }

        public ILogger Log { get; set; }

        public ISerializationProvider Serialization { get; set; }

        public IUriEncoder UriEncoder { get; set; }

        public IHttp Http { get; set; }

        public void Validate()
        {
            if (Http == null)
            {
                throw new SlumberException("Http has not been set");
            }

            if (UriEncoder == null)
            {
                throw new SlumberException("UriEncoder has not been set");
            }

            if (Serialization == null)
            {
                throw new SlumberException("Serialization has not been set");
            }

            if (Log == null)
            {
                throw new SlumberException("Log has not been set");
            }
        }

        public bool IsError(int code)
        {
            return code == -1 || code >= 400;
        }

        public Func<string> UserAgent { get; set; }
    }
}
