namespace Slumber
{
    public interface IRestResponse<T> : IRestResponse
    {
        T Data { get; set; }
    }
}
