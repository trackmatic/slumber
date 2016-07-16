namespace Slumber.Http
{
    public interface IHttpPostProcessor
    {
        void Process(IHttp http, IRequest request, IResponse response);
    }
}
