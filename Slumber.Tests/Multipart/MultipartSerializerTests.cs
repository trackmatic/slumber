using System;
using System.Drawing;
using Slumber.Http;
using Slumber.Multipart;
using Xunit;

namespace Slumber.Tests.Multipart
{
    public class MultipartSerializerTests
    {
        [Fact]
        public void ItShouldSerializeFiles()
        {
            var content = new MultipartContent();
            content.FormData.Add("id", "1");
            content.Files.Add("file", new MultipartFile("file.png", "image/png", GetBytes(Properties.Resources.File)));
            var serializer = new MultipartSerializer();
            var request = HttpRequestBuilder<dynamic>.Post("/").Content(content).Build();
            var now = DateTime.Now;
            var result = serializer.Serialize(request, now);
            var boundary = "------------" + now.Ticks.ToString("x") + "--";
            Assert.EndsWith(boundary, result);
        }
        public static byte[] GetBytes(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
