using System.IO;
using System.Threading.Tasks;

namespace Slumber.Http
{
    public interface IHttpContentWriter
    {
        Task WriteAsync(Stream stream);
    }
}
