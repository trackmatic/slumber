namespace Slumber
{
    public interface IRestResponse<T> : IRestResponse
    {
        T Data { get; }

        TErrorType GetErrorData<TErrorType>();
    }
}
