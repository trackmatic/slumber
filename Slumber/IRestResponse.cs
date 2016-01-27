using System;
using System.Collections.Generic;

namespace Slumber
{
    public interface IRestResponse
    {
        int StatusCode { get; }

        string Content { get; set; }

        IList<HttpHeader> Headers { get; }

        SlumberException Exception { get; }

        HttpHeader GetHeader(string name);

        bool HasError { get; }
    }
}
