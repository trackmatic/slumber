using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Slumber;

namespace Slmber.Json
{
    public class JsonSerializerFactory : ISerializationFactory
    {
        private static readonly JsonSerializerSettings Settings;

        static JsonSerializerFactory()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            Settings.Converters.Add(new IsoDateTimeConverter());
        }

        public ISerializer CreateSerializer()
        {
            return new DynamicJsonSerializer(Settings);
        }

        public IDeserializer CreateDeserializer()
        {
            return new DynamicJsonDeserializer(Settings);
        }
    }
}
