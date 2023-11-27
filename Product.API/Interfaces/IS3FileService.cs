using Amazon.S3.Model;
using Amazon.S3;
using System.Net;

namespace Product.API.Interfaces
{
    public interface IS3FileService
    {
        public Task<string> UploadFileAsync(IFormFile file, string bucketName, string key);

        public Task<byte[]> DownloadFileAsync(string bucketName, string key);

        public Task<bool> DeleteFileAsync(string bucketName, string key, string versionId = null);
    }
}
