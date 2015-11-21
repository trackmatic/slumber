using System.Collections.Generic;
using Newtonsoft.Json;
using Slmber.Json;
using Slumber.Http;
using Slumber.Logging;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SlumberClient("http://10.10.0.76:8001", slumber =>
            {
                slumber.UseJsonSerialization().UseHttp(http => http.UseJsonAsDefaultContentType()).UseConsoleLogger();
            });
            
            // Using Dynamic Types

            var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();

            var dynamicResult = client.Execute(dynamicRequest);

            // Using Defined Types

            var typedRequest = HttpRequestBuilder<ExchangeRates>.Get("/latest").QueryParameter("base", "USD").Build();

            var typesResult = client.Execute(typedRequest);
        }

        public class ExchangeRates
        {
            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("rates")]
            public Dictionary<string, double> Rates { get; set; }
        }
    }
}
