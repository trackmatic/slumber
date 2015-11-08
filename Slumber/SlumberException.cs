using System;

namespace Slumber
{
    public class SlumberException : Exception
    {
        public SlumberException(string message, params object[] parameters) : base(string.Format(message, parameters))
        {
            
        }
    }
}
