using System.Collections.Generic;
using Newtonsoft.Json;
using Slumber.Http;
using Slumber.Json;
using Slumber.Logging;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SlumberClient("https://api.github.com", slumber =>
            {
                slumber.UseJsonSerialization().UseHttp(http => http.UseJsonAsDefaultContentType()).UseConsoleLogger();
            });


            /*var response = client.ExecuteAsync(HttpRequestBuilder<dynamic>.Post("/account/auth").Content(new
            {
                username = "rossj@trackmatic.co.za",
                password = "W@kogofU71"
            }).Build()).Result;*/

            // Using Dynamic Types

            //var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();




            var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/repos/trackmatic/slumber/releases").Build();

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
