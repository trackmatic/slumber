using System.Web;

namespace Slumber.Http
{
    public class HttpParameterEncoder : IParameterEncoder
    {
        public string Encode(string path, RestQueryParameter parameter)
        {
            return path.Replace($"{{{parameter.Name}}}", Encode(parameter.Value));
        }

        public string Encode(RestQueryParameter parameter)
        {
            return $"{parameter.Name}={Encode(parameter.Value)}";
        }

        private static string Encode(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return HttpUtility.UrlEncode(value.ToString());
        }
    }
}
