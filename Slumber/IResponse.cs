namespace Slumber
{
    /// <summary>
    /// An abstraction of an http response
    /// </summary>
    public interface IResponse : IHeaders
    {
        int StatusCode { get; }

        string Content { get; set; }

        SlumberException Exception { get; }

        bool HasError { get; }
    }
}
