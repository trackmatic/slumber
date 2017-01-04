using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Slumber.Multipart
{
    public class MultipartSerializer : ISerializer
    {
        private readonly string _boundary;

        public MultipartSerializer(string boundary)
        {
            _boundary = boundary;
        }

        public Task Serialize(Stream stream, IRequest request)
        {
            return Serialize(stream, request, DateTime.Now);
        }

        public Task Serialize(Stream stream, IRequest request, DateTime now)
        {
            var content = GetMultipartContent(request);
            var builder = new Builder(_boundary, content, stream);
            return builder.WriteContent();
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
            private readonly Stream _stream;

            public Builder(string boundary, IMultipartContent content, Stream stream)
            {
                _boundary = boundary;
                _content = content;
                _stream = stream;
            }

            public async Task WriteContent()
            {
                var boundary = "--" + _boundary;

                foreach (var item in _content.Files)
                {
                    await Write(boundary).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await WriteFormat("Content-Disposition: form-data; name=\"{0}\";", item.Key).ConfigureAwait(false);
                    await WriteFormat(" filename=\"{0}\"", item.Value.Filename).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await WriteFormat("Content-Type: {0}", item.Value.ContentType).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await _stream.WriteAsync(item.Value.Data, 0, item.Value.Data.Length).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                }

                foreach (var item in _content.FormData)
                {
                    //await WriteLine().ConfigureAwait(false);
                    await Write(boundary).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await WriteFormat("Content-Disposition: form-data; name=\"{0}\"", item.Key).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                    await Write(item.Value).ConfigureAwait(false);
                    await WriteLine().ConfigureAwait(false);
                }
                
                await WriteFormat("{0}--", boundary).ConfigureAwait(false);
                await WriteLine().ConfigureAwait(false);
            }

            private Task WriteLine()
            {
                return Write("\r\n");
            }

            private Task WriteFormat(string value, params object[] parameters)
            {
                return Write(string.Format(value, parameters));
            }

            private Task Write(string value)
            {
                var buffer = Encoding.UTF8.GetBytes(value);
                return _stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
    }
}