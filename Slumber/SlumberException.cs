using System;

namespace Slumber
{
    public class SlumberException : Exception
    {
        public SlumberException(Exception e) : base(e.Message, e)
        {
            
        }

        public SlumberException(string message, params object[] parameters) : base(string.Format(message, parameters))
        {
            
        }

        public virtual T GetContent<T>(IDeserializer deserializer)
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
