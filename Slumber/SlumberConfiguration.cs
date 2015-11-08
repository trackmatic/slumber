﻿using System;

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
    }
}
