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

        public DynamicJsonSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public string Serialize(IRequest request)
        {
            if (request.Data == null) return null;
            return JsonConvert.SerializeObject(request.Data, Formatting.Indented, _settings);
        }
    }
}