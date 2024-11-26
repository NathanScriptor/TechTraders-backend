using Azure.Storage.Blobs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.Others
{
    public class BlobStorageHelper : IBlobStorageHelper
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageHelper(IConfiguration configuration)
        {
            // Retrieve connection string and container name from appsettings
            var connectionString = configuration.GetValue<string>("AzureBlobStorage:ConnectionString");
            _containerName = configuration.GetValue<string>("AzureBlobStorage:ContainerName");

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null)
                return string.Empty;

            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();

                // Generate a unique filename
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                // Create a BlobClient to upload the file
                var blobClient = containerClient.GetBlobClient(fileName);

                // Upload the file stream to the blob
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                // Optionally, set metadata for the blob
                await blobClient.SetMetadataAsync(new Dictionary<string, string>
                {
                    { "key1", "value1" }, // Add your key-value pairs here
                    { "key2", "value2" }
                });

                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message; // Return the error message if something goes wrong
            }
        }

        // Method to retrieve metadata of a blob
        public async Task<IDictionary<string, string>> GetMetadataAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var blobProperties = await blobClient.GetPropertiesAsync();

            return blobProperties.Value.Metadata;
        }

        // Method to delete a file from Blob Storage
        public async Task DeleteFileAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
