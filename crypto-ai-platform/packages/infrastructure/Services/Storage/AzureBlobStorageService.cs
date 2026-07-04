using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Services.Storage;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly AzureBlobOptions _options;
    private readonly ILogger<AzureBlobStorageService> _logger;

    public AzureBlobStorageService(IOptions<StorageOptions> options, ILogger<AzureBlobStorageService> logger)
    {
        _options = options.Value.AzureBlob ?? throw new InvalidOperationException("Azure Blob options not configured");
        _blobServiceClient = new BlobServiceClient(_options.ConnectionString);
        _logger = logger;
    }

    private async Task<BlobContainerClient> GetContainerAsync(CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_options.ContainerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        return containerClient;
    }

    private string BuildBlobPath(TenantId tenantId, string path)
    {
        return $"{tenantId.Value}/{path.TrimStart('/')}";
    }

    public async Task UploadAsync(TenantId tenantId, string path, Stream content, CancellationToken cancellationToken = default)
    {
        var blobPath = BuildBlobPath(tenantId, path);
        try
        {
            var containerClient = await GetContainerAsync(cancellationToken);
            var blobClient = containerClient.GetBlobClient(blobPath);
            await blobClient.UploadAsync(content, overwrite: true, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading to Azure Blob at path {Path}", blobPath);
            throw;
        }
    }

    public async Task<Stream?> DownloadAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var blobPath = BuildBlobPath(tenantId, path);
        try
        {
            var containerClient = await GetContainerAsync(cancellationToken);
            var blobClient = containerClient.GetBlobClient(blobPath);
            if (!await blobClient.ExistsAsync(cancellationToken))
                return null;

            var response = await blobClient.DownloadAsync(cancellationToken);
            return response.Value.Content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading from Azure Blob at path {Path}", blobPath);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var blobPath = BuildBlobPath(tenantId, path);
        var containerClient = await GetContainerAsync(cancellationToken);
        var blobClient = containerClient.GetBlobClient(blobPath);
        return await blobClient.ExistsAsync(cancellationToken);
    }

    public async Task DeleteAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        var blobPath = BuildBlobPath(tenantId, path);
        try
        {
            var containerClient = await GetContainerAsync(cancellationToken);
            var blobClient = containerClient.GetBlobClient(blobPath);
            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting from Azure Blob at path {Path}", blobPath);
            throw;
        }
    }
}
