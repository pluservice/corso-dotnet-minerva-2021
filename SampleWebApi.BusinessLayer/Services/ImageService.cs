using System.IO;
using System.Threading.Tasks;
using SampleWebApi.BusinessLayer.Providers;

namespace SampleWebApi.BusinessLayer.Services
{
    public class ImageService : IImageService
    {
        private readonly IStorageProvider storageProvider;

        public ImageService(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public async Task SaveAsync(Stream stream, string fileName)
        {
            await storageProvider.SaveAsync(fileName, stream);
        }

        public async Task<Stream> ReadAsync(string fileName)
        {
            var content = await storageProvider.ReadAsync(fileName);
            return content;
        }

        public async Task DeleteAsync(string fileName)
        {
            await storageProvider.DeleteAsync(fileName);
        }
    }
}
