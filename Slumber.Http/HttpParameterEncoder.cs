using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slumber.Http
{
    public class HttpParameterEncoder : IParameterEncoder
    {
        private readonly List<IEncoder> _encoders;

        public HttpParameterEncoder()
        {
            _encoders = new List<IEncoder> {new BooleanEncoder(), new HttpUtilityEncoder()};
        }

        public string Encode(string path, QueryParameter parameter)
        {
            return path.Replace($"{{{parameter.Name}}}", Encode(parameter.Value));
        }

        public string Encode(QueryParameter parameter)
        {
            return $"{parameter.Name}={Encode(parameter.Value)}";
        }

        private string Encode(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return _encoders.Where(x => x.IsApplicable(value.GetType())).Aggregate(value.ToString(), (current, encoder) => encoder.Encode(current));
        }

        private interface IEncoder
        {
            string Encode(object value);

            bool IsApplicable(Type type);
        }

        private class BooleanEncoder : IEncoder
        {
            public string Encode(object value)
            {
                return value.ToString().ToLower();
            }

            public bool IsApplicable(Type type)
            {
                return type == typeof (bool);
            }
        }

        private class HttpUtilityEncoder : IEncoder
        {
            public string Encode(object value)
            {
                return HttpUtility.UrlEncode(value.ToString());
            }

            public bool IsApplicable(Type type)
            {
                return true;
            }
        }
    }
}
