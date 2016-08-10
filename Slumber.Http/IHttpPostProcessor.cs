namespace Slumber.Http
{
    public interface IHttpPostProcessor
    {
        void OnExecuted(IHttp http, IRequest request, IResponse response);
    }
}
