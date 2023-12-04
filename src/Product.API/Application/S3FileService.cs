using Amazon.S3;
using Amazon.S3.Model;
using Product.API.Interfaces;
using System.Net;

namespace Product.API.Application
{
    public class S3FileService : IS3FileService
    {
        #region constructor
        private readonly IAmazonS3 _s3Client;
        public S3FileService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        #endregion

        #region UploadFileAsync
        public async Task<string> UploadFileAsync(IFormFile file, string bucketName, string key)
        {
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) return $"Bucket {bucketName} does not exist.";

                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(request);
                string s3Uri = $"https://{bucketName}.s3.amazonaws.com/{key}";
                return s3Uri;
            }
            catch (AmazonS3Exception)
            {
                throw;
            }
        }
        #endregion

        #region DownloadFileAsync
        public async Task<byte[]> DownloadFileAsync(string bucketName, string key)
        {
            MemoryStream ms = null;

            try
            {
                string s3ObjectKey = ConvertS3UriToS3ObjectKey(key);
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3ObjectKey
                };

                using (var response = await _s3Client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                {
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", key));
                }    
                    
                return ms.ToArray();
            }
            catch (AmazonS3Exception)
            {
                throw;
            }
        }
        #endregion

        #region DeleteFileAsync
        public async Task<bool> DeleteFileAsync(string bucketName, string key, string versionId = null)
        {
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists) return false;

                string s3ObjectKey = ConvertS3UriToS3ObjectKey(key);
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3ObjectKey
                };

                if (!string.IsNullOrEmpty(versionId))
                    request.VersionId = versionId;

                await _s3Client.DeleteObjectAsync(bucketName, key);
                return true;
            }
            catch (AmazonS3Exception)
            {
                return false;
            }
        }
        #endregion

        #region ConvertS3UriToS3ObjectKey
        private string ConvertS3UriToS3ObjectKey(string key)
        {
            var temp = key.Split('/');
            string s3Key = temp[temp.Length - 1];
            return s3Key;
        }
        #endregion
    }
}
