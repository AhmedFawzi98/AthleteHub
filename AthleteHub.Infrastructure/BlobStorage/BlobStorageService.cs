using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Infrastructure.Configurations;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Infrastructure.BlobStorage
{
    internal class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings.Value;
        }

        public async Task DeleteBlobAsync(string blobUrl)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var containerName = new Uri(blobUrl).Segments[1].Trim('/');
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            string fileName = new Uri(blobUrl).Segments.Last();

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> UploadToBlobAsync(string fileName, Stream file, string contentType)
        {
            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var isValidContentType = _blobStorageSettings.ContainerMappings.TryGetValue(contentType, out string containerName);
            if (!isValidContentType)
                return null;
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var uploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType,
                    ContentDisposition = $"inline; filename={fileName}"
                }
            };

            await blobClient.UploadAsync(file, uploadOptions);
            var blobUrl = blobClient.Uri.ToString();

            return blobUrl;
        }
        public string GetBlobSasUrl(string? blobUrl)
        {
            var containerName = new Uri(blobUrl).Segments[1].Trim('/');

            if (string.IsNullOrEmpty(blobUrl))
                return null;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobName = new Uri(blobUrl).Segments.Last(),
                BlobContainerName = containerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(15)
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(_blobStorageSettings.AccountName, _blobStorageSettings.AccountKey))
                                     .ToString();

            return $"{blobUrl}?{sasToken}";
        }
    }
}
