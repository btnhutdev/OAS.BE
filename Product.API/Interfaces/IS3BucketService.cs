namespace Product.API.Interfaces
{
    public interface IS3BucketService
    {
        Task<bool> CreateBucketAsync(string bucketName);
        Task<IList<string>> GetAllBucketAsync();
        Task<bool> DeleteBucketAsync(string bucketName);
        Task<bool> CheckConnectS3BucketAsync();
    }
}
