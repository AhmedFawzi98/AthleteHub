using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.BlobStorageService
{
    public interface IBlobStorageService
    {
        Task<String> UploadToBlobAsync(string fileName, Stream file, string contentType);
        Task DeleteBlobAsync(string logoUrl);
        string GetBlobSasUrl(string? blobUrl);
    }
}
