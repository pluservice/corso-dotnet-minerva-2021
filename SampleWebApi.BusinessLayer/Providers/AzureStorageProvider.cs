using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace SampleWebApi.BusinessLayer.Providers
{
    public class AzureStorageProvider : IStorageProvider
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly string containerName;

        public AzureStorageProvider(IConfiguration configuration)
        {
            blobServiceClient = new BlobServiceClient(configuration.GetConnectionString("StorageConnectionString"));
            containerName = configuration.GetValue<string>("ApplicationOptions:ContainerName");
        }

        public async Task DeleteAsync(string path)
        {
            var blobClient = await GetBlobClientAsync();
            await blobClient.DeleteBlobIfExistsAsync(path);
        }

        public async Task<Stream> ReadAsync(string path)
        {
            var blobContainerClient = await GetBlobClientAsync();
            var blobClient = blobContainerClient.GetBlobClient(path);

            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);

            stream.Position = 0;
            return stream;
        }

        public async Task SaveAsync(string path, Stream stream)
        {
            var blobContainerClient = await GetBlobClientAsync();
            var blobClient = blobContainerClient.GetBlobClient(path);

            stream.Position = 0;

            await blobClient.UploadAsync(stream, new BlobHttpHeaders
            {
                ContentType = MimeMapping.MimeUtility.GetMimeMapping(path)
            });
        }

        private async Task<BlobContainerClient> GetBlobClientAsync()
        {
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            return blobContainerClient;
        }
    }
}
