using System.Collections.Generic;

namespace Slumber
{
    /// <summary>
    /// An abstraction of an http response
    /// </summary>
    public interface IResponse
    {
        int StatusCode { get; }

        string Content { get; set; }

        IList<HttpHeader> Headers { get; }

        SlumberException Exception { get; }

        HttpHeader GetHeader(string name);

        bool ContainsHeader(string name);

        bool HasError { get; }
    }
}
