using Amazon.S3;
using Product.API.Interfaces;
using Product.API.Utilities;

namespace Product.API.Application
{
    public class S3BucketService : IS3BucketService
    {
        #region constructor
        private readonly IAmazonS3 _s3Client;

        public S3BucketService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        #endregion

        #region CheckConnectS3BucketAsync
        public async Task<bool> CheckConnectS3BucketAsync()
        {
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(BucketNameConst.BucketName);
                return bucketExists;
            }
            catch (AmazonS3Exception)
            {
                return false;
            }
        }
        #endregion

        #region CreateBucketAsync
        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (bucketExists) return false;
            await _s3Client.PutBucketAsync(bucketName);
            return true;
        }
        #endregion

        #region GetAllBucketAsync
        public async Task<IList<string>> GetAllBucketAsync()
        {
            var data = await _s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => { return b.BucketName; });
            return new List<string>(buckets);
        }
        #endregion

        #region DeleteBucketAsync
        public async Task<bool> DeleteBucketAsync(string bucketName)
        {
            try
            {
                await _s3Client.DeleteBucketAsync(bucketName);
                return true;
            }
            catch (AmazonS3Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
