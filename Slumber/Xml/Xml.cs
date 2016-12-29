using System.IO;
using System.Text;
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
        public string Serialize(IRequest request)
        {
            using (var stream = new MemoryStream())
            {
                if (request.Data == null) return null;
                var serializer = new XmlSerializer(request.Data.GetType());
                serializer.Serialize(stream, request.Data);
                var buffer = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }
    }
}