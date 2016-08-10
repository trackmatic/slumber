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
            var client = new SlumberClient("https://rest.trackmatic.co.za/api/v2", slumber =>
            {
                slumber.UseJsonSerialization().UseHttp(http => http.UseJsonAsDefaultContentType()).UseConsoleLogger();
            });


            var response = client.ExecuteAsync(HttpRequestBuilder<dynamic>.Post("/account/auth").Content(new
            {
                username = "rossj@trackmatic.co.za",
                password = "W@kogofU71"
            }).Build()).Result;

            // Using Dynamic Types

            var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();

            var dynamicResult = client.ExecuteAsync(dynamicRequest).Result;

            // Using Defined Types

            var typedRequest = HttpRequestBuilder<ExchangeRates>.Get("/latest").QueryParameter("base", "USD").Build();

            var typesResult = client.ExecuteAsync(typedRequest).Result;


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
