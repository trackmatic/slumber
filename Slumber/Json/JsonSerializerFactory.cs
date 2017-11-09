using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Slumber.Json
{
    public class JsonSerializerFactory : ISerializationFactory
    {
        private readonly JsonSerializerSettings _settings;
        private readonly ISlumberConfiguration _configuration;

        public JsonSerializerFactory(ISlumberConfiguration configuration, Action<JsonSerializerSettings> customise = null)
        {
            _configuration = configuration;
            _settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            _settings.Converters.Add(new IsoDateTimeConverter());
            customise?.Invoke(_settings);
        }

        public ISerializer CreateSerializer(IRequest request)
        {
            return new DynamicJsonSerializer(_settings, _configuration);
        }

        public IDeserializer CreateDeserializer()
        {
            return new DynamicJsonDeserializer(_settings);
        }
    }
}
