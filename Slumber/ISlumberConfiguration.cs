using System;

namespace Slumber
{
    /// <summary>
    /// Abstraction of the confugruation layer
    /// </summary>
    public interface ISlumberConfiguration
    {
        /// <summary>
        /// Base uri of the api you are calling
        /// </summary>
        string BaseUri { get; }

        /// <summary>
        /// Timeout value for requests
        /// </summary>
        TimeSpan Timeout { get; }

        /// <summary>
        /// An instance of a logger
        /// </summary>
        ILogger Log { get; set; }

        /// <summary>
        /// An instance of a serializer factory
        /// </summary>
        ISerializationProvider Serialization { get; set; }

        /// <summary>
        /// An instance of a uri encoder
        /// </summary>
        IUriEncoder UriEncoder { get; set; }

        /// <summary>
        /// An instance of an http abstraction
        /// </summary>
        IHttp Http { get; set; }

        /// <summary>
        /// Validates that the configuration is valid, this is tested before any requests can be made
        /// </summary>
        void Validate();

        /// <summary>
        /// Determines whether the given code should be treated as an error
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool IsError(int code);
    }
}
