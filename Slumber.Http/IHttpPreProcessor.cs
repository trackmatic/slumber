namespace Slumber.Http
{
    public interface IHttpPreProcessor
    {
        void Process(IHttp http, IRestRequest request);
    }
}
