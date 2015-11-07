using System.Linq;
using System.Text;

namespace Slumber
{
    /// <summary>
    /// Takes a rest request and builds up a url with encoded query parameters
    /// </summary>
    public class DefaultUriEncoder : IUriEncoder
    {
        private readonly IParameterEncoder _parameterEncoder;

        public DefaultUriEncoder(IParameterEncoder parameterEncoder)
        {
            _parameterEncoder = parameterEncoder;
        }

        /// <summary>
        /// Creates a url for the provided rest request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string Encode(IRestRequest request)
        {
            var sb = new StringBuilder();
            var query = request.Query.ToList();
            var path = request.Path;
            if (query.Count > 0)
            {
                sb.Append("?");
            }
            foreach (var item in query)
            {
                if (path.Contains($"{{{item.Name}}}"))
                {
                    path = _parameterEncoder.Encode(path, item);
                }
                else
                {
                    sb.Append(_parameterEncoder.Encode(item)).Append("&");
                }
            }
            var join = string.Concat(path, sb.ToString());
            return join.TrimEnd('&').TrimEnd('?');
        }
    }
}
