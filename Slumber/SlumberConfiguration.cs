using System;

namespace Slumber
{
    public class SlumberConfiguration : ISlumberConfiguration
    {
        public SlumberConfiguration(string baseUri, TimeSpan timeout)
        {
            BaseUri = baseUri;
            Timeout = timeout;
            Log = new NullLogger();
        }

        public string BaseUri { get; }

        public TimeSpan Timeout { get; }

        public ILogger Log { get; set; }

        public ISerializationFactory Serialization { get; set; }

        public IUriEncoder UriEncoder { get; set; }

        public IHttp Http { get; set; }

        public void Validate()
        {
            if (Http == null)
            {
                throw new NapException("Http has not been set");
            }

            if (UriEncoder == null)
            {
                throw new NapException("UriEncoder has not been set");
            }

            if (Serialization == null)
            {
                throw new NapException("Serialization has not been set");
            }

            if (Log == null)
            {
                throw new NapException("Log has not been set");
            }
        }
    }
}
