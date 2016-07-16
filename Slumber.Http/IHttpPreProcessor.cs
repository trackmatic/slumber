namespace Slumber.Http
{
    public interface IHttpPreProcessor
    {
        void Process(IHttp http, IRequest request);
    }
}
