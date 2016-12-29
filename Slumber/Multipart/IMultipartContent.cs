using System.Collections.Generic;

namespace Slumber.Multipart
{
    public interface IMultipartContent
    {
        IDictionary<string, string> FormData { get; set; }
        IDictionary<string, IMultipartFile> Files { get; set; }
    }
}
