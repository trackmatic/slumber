namespace Slumber
{
    public class UpstreamException : SlumberException
    {

        public UpstreamException(string content)
            : base("An upstream error occured, please refer to the Content property for more information")
        {
            Content = content;
        }

        public string Content { get; }

        public override T GetContent<T>(IDeserializer deserializer)
        {
            return deserializer.Deserialize<T>(Content);
        }
    }
}
