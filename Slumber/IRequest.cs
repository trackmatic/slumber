using System;
using System.Collections.Generic;

namespace Slumber
{
    /// <summary>
    /// Abstraction of an http request
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// The path to the resource being worked on
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The method the http request must use
        /// </summary>
        string Method { get; }

        /// <summary>
        /// The data to send upstream if applicable
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// A list of query parameters
        /// </summary>
        IEnumerable<QueryParameter> Query { get; }

        /// <summary>
        /// A list of headers to be sent upstream
        /// </summary>
        IEnumerable<HttpHeader> Headers { get; }

        /// <summary>
        /// A list of cookies associated with the request
        /// </summary>
        IEnumerable<HttpCookie> Cookies { get; }

        /// <summary>
        /// The type to expect back
        /// </summary>
        Type ResponseType { get; }

        /// <summary>
        /// Adds a cookie to the request
        /// </summary>
        /// <param name="cookie"></param>
        void Add(HttpCookie cookie);

        /// <summary>
        /// Checks if the request contains a cookie
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        bool Contains(HttpHeader header);

        /// <summary>
        /// Adds a header to the request
        /// </summary>
        /// <param name="header"></param>
        void Add(HttpHeader header);

        /// <summary>
        /// Adds a header to the request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void AddHeader(string name, string value);

        /// <summary>
        /// Adds a query parameter to the request
        /// </summary>
        /// <param name="parameter"></param>
        void AddQueryParameter(QueryParameter parameter);

        /// <summary>
        /// Adds a query parameter to the request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="ignoreEmptyValues"></param>
        void AddQueryParameter(string name, object value, bool ignoreEmptyValues = false);

        /// <summary>
        /// Retrieves a header from the request
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        HttpHeader GetHeader(string name);
    }
}
