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
        private readonly ISlumberConfiguration _configuration;

        public DynamicJsonSerializer(JsonSerializerSettings settings, ISlumberConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
        }

        public Task Serialize(Stream stream, IRequest request)
        {
            if (request.Data == null) return null;
            var data = JsonConvert.SerializeObject(request.Data, Formatting.Indented, _settings);
            _configuration?.Log?.Debug("Data: {0}", data);
            var buffer = Encoding.UTF8.GetBytes(data);
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}