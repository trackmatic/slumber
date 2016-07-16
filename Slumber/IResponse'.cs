namespace Slumber
{
    public interface IResponse<T> : IResponse
    {
        T Data { get; }

        TErrorType GetErrorData<TErrorType>();
    }
}
