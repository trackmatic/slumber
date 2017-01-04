using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Slumber.Json
{
    public class DynamicJsonDeserializer : IDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public DynamicJsonDeserializer(JsonSerializerSettings settings)
        {
            _settings = settings;
        }
        
        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content, _settings);
        }
    }

    public class DynamicJsonSerializer : ISerializer
    {        
        private readonly JsonSerializerSettings _settings;
        private readonly ILogger _logger;

        public DynamicJsonSerializer(JsonSerializerSettings settings, ILogger logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public string Serialize(IRequest request)
        {
            if (request.Data == null) return null;
            return JsonConvert.SerializeObject(request.Data, Formatting.Indented, _settings);
        }

        public Task Serialize(Stream stream, IRequest request)
        {
            if (request.Data == null) return null;
            var data = JsonConvert.SerializeObject(request.Data, Formatting.Indented, _settings);
            _logger.Debug(data);
            var buffer = Encoding.UTF8.GetBytes(data);
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}