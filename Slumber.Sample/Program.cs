using Slmber.Json;
using Slumber.Http;
using Slumber.Logging;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SlumberClient("https://rest.trackmatic.co.za/api/v1", nap =>
            {
                nap.UseJsonSerialization().UseHttp(http => http.ApplicationJson()).UseConsoleLogger();
            });

            client.Execute(new HttpRestRequest<dynamic>("/core/security/authenticate", HttpMethods.Post)
            {
                Data = new
                {
                    Username = "8310245219089",
                    Password = "W@kogofU71"
                }
            });

            var resposne = client.Execute(new HttpRestRequest<dynamic>("/core/account/get", HttpMethods.Get));
        }
    }
}
