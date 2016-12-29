using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Slumber.Http;
using Slumber.Json;
using Slumber.Logging;
using Slumber.Multipart;
using Slumber.Xml;

namespace Slumber.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var client = new SlumberClient("http://api.fixer.io", slumber =>
            {
                slumber.UseJsonSerialization().UseXmlSerialization().UseHttp(http => http.UseJsonAsDefaultContentType()).UseConsoleLogger();
            });

            // Using Dynamic Types

            var dynamicRequest = HttpRequestBuilder<dynamic>.Get("/latest").QueryParameter("base", "USD").Build();
            var dynamicResult = client.ExecuteAsync(dynamicRequest).Result;

            // Using Defined Types

            var typedRequest = HttpRequestBuilder<ExchangeRates>.Get("/latest").QueryParameter("base", "USD").Build();
            var typesResult = client.ExecuteAsync(typedRequest).Result.Data;*/

            var client = new SlumberClient("http://ps.uci.edu/~franklin/doc", slumber =>
            {
                slumber.UseMultipartSerialization()
                    .UseJsonSerialization()
                    .UseXmlSerialization()
                    .UseHttp(http => http.UseJsonAsDefaultContentType())
                    .UseConsoleLogger();
            });


            var content = new MultipartContent();
            content.FormData.Add("id", "1");
            content.Files.Add("file", new MultipartFile("file.png", "image/png", File.ReadAllBytes("file.png")));

            var dynamicRequest = HttpRequestBuilder<dynamic>.Post("/file_upload.html").Content(content).Build();

            var response = client.ExecuteAsync(dynamicRequest).Result;
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