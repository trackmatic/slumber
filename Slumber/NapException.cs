using System;

namespace Slumber
{
    public class NapException : Exception
    {
        public NapException(string message, params object[] parameters) : base(string.Format(message, parameters))
        {
            
        }
    }
}
