using System;
using System.Text;

namespace Slumber.Multipart
{
    public class MultipartSerializer : ISerializer
    {

        public string Serialize(IRequest request)
        {
            return Serialize(request, DateTime.Now);
        }

        public string Serialize(IRequest request, DateTime now)
        {
            request.RemoveHeader(HttpHeaders.ContentType);
            var boundary = "----------" + now.Ticks.ToString("x");
            var header = $"multipart/form-data; boundary={boundary}";
            request.AddHeader(HttpHeaders.ContentType, header);
            var content = GetMultipartContent(request);
            var builder = new Builder(boundary, content);
            return builder.GetContent();
        }

        private static IMultipartContent GetMultipartContent(IRequest request)
        {
            var content = request.Data as IMultipartContent;
            if (content == null)
            {
                throw new SlumberException("Data must be an instance of IMultipartContent");
            }
            return content;
        }

        private class Builder
        {
            private readonly string _boundary;
            private readonly IMultipartContent _content;

            public Builder(string boundary, IMultipartContent content)
            {
                _boundary = boundary;
                _content = content;
            }

            public string GetContent()
            {
                var data = new StringBuilder();

                foreach (var item in _content.FormData)
                {
                    data.Append(_boundary);
                    data.AppendLine();
                    data.AppendFormat("Content-Disposition: form-data; name={0}", item.Key);
                    data.AppendLine();
                    data.AppendLine();
                    data.Append(item.Value);
                    data.AppendLine();
                }

                foreach (var item in _content.Files)
                {
                    data.Append(_boundary);
                    data.AppendLine();
                    data.AppendFormat("Content-Disposition: form-data; name=\"{0}\";", item.Key);
                    data.AppendFormat(" filename=\"{0}\";", item.Value.Filename);
                    data.AppendLine();
                    data.AppendFormat("Content-Type: {0}", item.Value.ContentType);
                    data.AppendLine();
                    data.AppendLine();
                    data.Append(Encoding.UTF8.GetString(item.Value.Data));
                    data.AppendLine();
                }

                data.AppendFormat("--{0}--", _boundary);

                return data.ToString();
            }
        }
    }
}