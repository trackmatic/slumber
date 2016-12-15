using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Slumber.Json
{
    public class JsonSerializerFactory : ISerializationFactory
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializerFactory(Action<JsonSerializerSettings> customise = null)
        {
            _settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            _settings.Converters.Add(new IsoDateTimeConverter());
            customise?.Invoke(_settings);
        }

        public ISerializer CreateSerializer()
        {
            return new DynamicJsonSerializer(_settings);
        }

        public IDeserializer CreateDeserializer()
        {
            return new DynamicJsonDeserializer(_settings);
        }
    }
}
