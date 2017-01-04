using System.IO;
using System.Threading.Tasks;

namespace Slumber
{
    public interface ISerializer
    {
        Task Serialize(Stream stream, IRequest request);
    }
}
