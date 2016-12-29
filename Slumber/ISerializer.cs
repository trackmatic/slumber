namespace Slumber
{
    public interface ISerializer
    {
        string Serialize(IRequest request);
    }
}
