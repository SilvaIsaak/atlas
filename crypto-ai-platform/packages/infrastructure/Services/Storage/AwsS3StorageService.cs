using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Services.Storage;

public class AwsS3StorageService : IAwsS3StorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly AwsS3Options _options;
    private readonly ILogger<AwsS3StorageService> _logger;

    public AwsS3StorageService(IOptions<StorageOptions> options, ILogger<AwsS3StorageService> logger)
    {
        _options = options.Value.AwsS3 ?? throw new InvalidOperationException("AWS S3 options not configured");
        _s3Client = new AmazonS3Client(_options.AccessKey, _options.SecretKey, Amazon.RegionEndpoint.GetBySystemName(_options.Region));
        _logger = logger;
    }

    private string BuildS3Key(TenantId tenantId, string path)
    {
        return $"{tenantId.Value}/{path.TrimStart('/')}";
    }

    public async Task UploadAsync(TenantId tenantId, string path, Stream content, CancellationToken cancellationToken = default)
    {
        var s3Key = BuildS3Key(tenantId, path);
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = s3Key,
                InputStream = content
            };

            await _s3Client.PutObjectAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading to S3 at key {Key}", s3Key);
            throw;
        }
    }

    public async Task<Stream?> DownloadAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var s3Key = BuildS3Key(tenantId, path);
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _options.BucketName,
                Key = s3Key
            };

            var response = await _s3Client.GetObjectAsync(request, cancellationToken);
            return response.ResponseStream;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading from S3 at key {Key}", s3Key);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var s3Key = BuildS3Key(tenantId, path);
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = _options.BucketName,
                Key = s3Key
            };

            await _s3Client.GetObjectMetadataAsync(request, cancellationToken);
            return true;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    public async Task DeleteAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var s3Key = BuildS3Key(tenantId, path);
        try
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = s3Key
            };

            await _s3Client.DeleteObjectAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting from S3 at key {Key}", s3Key);
            throw;
        }
    }
}
