using System;
using System.Drawing;
using System.IO;
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
            var serializer = new MultipartSerializer("----1234");
            var request = HttpRequestBuilder<dynamic>.Post("/").Content(content).Build();
            var now = DateTime.Now;
            var stream = new MemoryStream();
            serializer.Serialize(stream, request, now).Wait();
            var boundary = "------1234--\r\n";
            stream.Position = 0;
            var result = new StreamReader(stream).ReadToEnd();
            Assert.EndsWith(boundary, result);
        }

        public static byte[] GetBytes(Image img)
        {
            var converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
