using System.Collections.Generic;

namespace Slumber.Multipart
{
    public class MultipartContent : IMultipartContent
    {
        public MultipartContent()
        {
            FormData = new Dictionary<string, string>();
            Files = new Dictionary<string, IMultipartFile>();
        }

        public IDictionary<string, string> FormData { get; set; }
        public IDictionary<string, IMultipartFile> Files { get; set; }
    }
}
