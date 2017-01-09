namespace Slumber
{
    public interface IUriEncoder
    {
        string Encode(IRequest request);
        IParameterEncoder ParameterEncoder { get; }
    }
}
