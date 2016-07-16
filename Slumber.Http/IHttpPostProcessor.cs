namespace Slumber.Http
{
    public interface IHttpPostProcessor
    {
        void Process(IHttp http, IRestRequest request, IRestResponse response);
    }
}
