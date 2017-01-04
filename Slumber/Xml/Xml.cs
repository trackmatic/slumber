using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Slumber.Xml
{
    public class HttpXmlDeserializer : IDeserializer
    {
        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrEmpty(content)) return default(T);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var serializer = new XmlSerializer(typeof (T));
                var result = (T) serializer.Deserialize(stream);
                return result;
            }
        }
    }

    public class HttpXmlSerializer : ISerializer
    {
        public Task Serialize(Stream stream, IRequest request)
        {
            return Task.Run(() =>
            {
                if (request.Data == null) return;
                var serializer = new XmlSerializer(request.Data.GetType());
                serializer.Serialize(stream, request.Data);
            });
        }
    }
}