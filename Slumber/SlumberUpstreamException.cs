using System;
using System.Runtime.Serialization;

namespace Slumber
{
    [Serializable]
    public class SlumberUpstreamException : SlumberException
    {
        public SlumberUpstreamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }


        public SlumberUpstreamException(string content)
            : base("An upstream error occured, please refer to the Content property for more information")
        {
            Content = content;
        }

        public string Content { get; }

        public override T GetContent<T>(IDeserializer deserializer)
        {
            return deserializer.Deserialize<T>(Content);
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
