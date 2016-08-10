namespace Slumber.Http
{
    public interface IHttpPreProcessor
    {
        void OnExecuting(IHttp http, IRequest request);
    }
}
