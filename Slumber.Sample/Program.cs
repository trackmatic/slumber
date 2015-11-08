using Slmber.Json;
using Slumber.Http;
using Slumber.Logging;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SlumberClient("https://someurl.com/api/v1", slumber =>
            {
                slumber.UseJsonSerialization().UseHttp(http => http.ApplicationJson()).UseConsoleLogger();
            });
        }
    }
}
