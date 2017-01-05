using System.Collections.Generic;
using Newtonsoft.Json;
using Slumber.Http;
using Slumber.Logging;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SlumberClient(SlumberConfigurationFactory.Default("http://api.fixer.io", configure: slumber =>
            {
                slumber.UseConsoleLogger();
                slumber.Serialization.Register(ContentTypes.TextHtml, slumber.Serialization.GetFactory(ContentTypes.ApplicationJson));
            }));

            // Using Dynamic Types

            var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();
            var dynamicResult = client.ExecuteAsync(dynamicRequest).Result;

            var d = dynamicResult.Data;

            // Using Defined Types

            var typedRequest = HttpRequestBuilder<ExchangeRates>.Get("/latest").QueryParameter("base", "USD").Build();
            var typesResult = client.ExecuteAsync(typedRequest).Result.Data;
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