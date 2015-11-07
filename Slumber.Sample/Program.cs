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
        }
    }
}
