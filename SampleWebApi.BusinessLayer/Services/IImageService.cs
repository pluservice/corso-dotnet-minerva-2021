using System.IO;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public interface IImageService
    {
        Task DeleteAsync(string fileName);

        Task<Stream> ReadAsync(string fileName);

        Task SaveAsync(Stream stream, string fileName);
    }
}