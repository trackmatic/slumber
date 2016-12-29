using System.Collections.Generic;

namespace Slumber
{
    public interface IHeaders
    {
        /// <summary>
        /// Retrieves a header from the request
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        HttpHeader GetHeader(string name);

        /// <summary>
        /// Checks if the request contains a cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ContainsHeader(string name);

        /// <summary>
        /// A list of headers to be sent upstream
        /// </summary>
        IList<HttpHeader> Headers { get; }
    }
}
