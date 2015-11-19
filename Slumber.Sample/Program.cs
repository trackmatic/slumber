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
                slumber.UseJsonSerialization().UseHttp(http => http.ApplicationJson()).UseConsoleLogger();
            });

            var request = HttpRequestBuilder<dynamic>.Post("/apis").Content(new
            {
                name = "api/v1/avt",
                upstream_url = "http://avt.trackmatic.co.za",
                preserve_host = false,
                created_at = 0,
                request_path = "/api/v2/avt",
                enabled = true
            }).Build();

            client.Execute(request);

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
