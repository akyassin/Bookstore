using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Bookstore.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Repos
{
    public class BlobRepository : IBlobRepository
    {
        private readonly BlobServiceClient blobClient;

        public BlobRepository(BlobServiceClient blobClient)
        {
            this.blobClient = blobClient;
        }

        public async Task<IEnumerable<string>> GetaAllBlob(string containerName)
        {
            var containerClient = blobClient.GetBlobContainerClient(containerName);
            var blobs = containerClient.GetBlobsAsync();

            List<string> files = new();
            await foreach (var blob in blobs)
            {
                files.Add(blob.Name);
            }

            return files;
        }

        public string GetBlob(string name, string containerName)
        {
            var containerClient = blobClient.GetBlobContainerClient(containerName);
            var bClient = containerClient.GetBlobClient(name);

            return bClient.Uri.AbsoluteUri;
        }

        public async Task<bool> UploadBlob(string name, IFormFile file, string containerName)
        {
            var containerClient = blobClient.GetBlobContainerClient(containerName);
            var bClient = containerClient.GetBlobClient(name);

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            var res = await bClient.UploadAsync(file.OpenReadStream(), httpHeaders);

            if (res != null)
                return true;

            return false;
        }

        public async Task<bool> DeleteBlob(string name, string containerName)
        {
            var containerClient = blobClient.GetBlobContainerClient(containerName);
            var bClient = containerClient.GetBlobClient(name);

            return await bClient.DeleteIfExistsAsync();
        }
    }
}
