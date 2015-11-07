namespace Slumber
{
    public interface IUriEncoder
    {
        string Encode(IRestRequest request);
    }
}
