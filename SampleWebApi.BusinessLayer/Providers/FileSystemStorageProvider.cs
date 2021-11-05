using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SampleWebApi.BusinessLayer.Providers
{
    public class FileSystemStorageProvider : IStorageProvider
    {
        private readonly string storageFolder;

        public FileSystemStorageProvider(IConfiguration configuration)
        {
            storageFolder = configuration.GetValue<string>("ApplicationOptions:StorageFolder");
        }

        public Task DeleteAsync(string path)
        {
            var fullPath = Path.Combine(storageFolder, path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }

        public Task<Stream> ReadAsync(string path)
        {
            var fullPath = Path.Combine(storageFolder, path);
            if (!File.Exists(fullPath))
            {
                return null;
            }

            var stream = File.OpenRead(fullPath);
            return Task.FromResult(stream as Stream);
        }

        public async Task SaveAsync(string path, Stream stream)
        {
            var fullPath = Path.Combine(storageFolder, path);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using var outputStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);

            stream.Position = 0;
            await stream.CopyToAsync(outputStream);

            outputStream.Close();
        }
    }
}
