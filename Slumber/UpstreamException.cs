namespace Slumber
{
    public class UpstreamException : NapException
    {

        public UpstreamException(string content)
            : base("An upstream error occured, please refer to the Content property for more information")
        {
            Content = content;
        }

        public string Content { get; }
    }
}
