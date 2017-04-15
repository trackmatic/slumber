using System;

namespace Slumber.Http
{
    public class HttpTimeoutException : Exception
    {
        public HttpTimeoutException(string message) : base(message)
        {
            
        }
    }
}
