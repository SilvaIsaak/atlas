using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Core.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Services.Storage;

public class ColdStorageService : IColdStorageService
{
    private readonly IAzureBlobStorageService? _azureBlobService;
    private readonly IAwsS3StorageService? _awsS3Service;
    private readonly StorageOptions _options;
    private readonly ILogger<ColdStorageService> _logger;

    public ColdStorageService(IOptions<StorageOptions> options, ILogger<ColdStorageService> logger, IAzureBlobStorageService? azureBlobService = null, IAwsS3StorageService? awsS3Service = null)
    {
        _options = options.Value;
        _logger = logger;
        _azureBlobService = azureBlobService;
        _awsS3Service = awsS3Service;
    }

    public async Task UploadAsync(TenantId tenantId, string path, Stream content, CancellationToken cancellationToken = default)
    {
        switch (_options.Provider.ToLower())
        {
            case "azureblob":
                if (_azureBlobService == null)
                    throw new InvalidOperationException("Azure Blob service not configured");
                await _azureBlobService.UploadAsync(tenantId, path, content, cancellationToken);
                break;
            case "awss3":
                if (_awsS3Service == null)
                    throw new InvalidOperationException("AWS S3 service not configured");
                await _awsS3Service.UploadAsync(tenantId, path, content, cancellationToken);
                break;
            default:
                throw new NotSupportedException($"Storage provider {_options.Provider} is not supported");
        }
    }

    public async Task<Stream?> DownloadAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        switch (_options.Provider.ToLower())
        {
            case "azureblob":
                if (_azureBlobService == null)
                    throw new InvalidOperationException("Azure Blob service not configured");
                return await _azureBlobService.DownloadAsync(tenantId, path, cancellationToken);
            case "awss3":
                if (_awsS3Service == null)
                    throw new InvalidOperationException("AWS S3 service not configured");
                return await _awsS3Service.DownloadAsync(tenantId, path, cancellationToken);
            default:
                throw new NotSupportedException($"Storage provider {_options.Provider} is not supported");
        }
    }

    public async Task<bool> ExistsAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        switch (_options.Provider.ToLower())
        {
            case "azureblob":
                if (_azureBlobService == null)
                    throw new InvalidOperationException("Azure Blob service not configured");
                return await _azureBlobService.ExistsAsync(tenantId, path, cancellationToken);
            case "awss3":
                if (_awsS3Service == null)
                    throw new InvalidOperationException("AWS S3 service not configured");
                return await _awsS3Service.ExistsAsync(tenantId, path, cancellationToken);
            default:
                throw new NotSupportedException($"Storage provider {_options.Provider} is not supported");
        }
    }

    public async Task DeleteAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default)
    {
        switch (_options.Provider.ToLower())
        {
            case "azureblob":
                if (_azureBlobService == null)
                    throw new InvalidOperationException("Azure Blob service not configured");
                await _azureBlobService.DeleteAsync(tenantId, path, cancellationToken);
                break;
            case "awss3":
                if (_awsS3Service == null)
                    throw new InvalidOperationException("AWS S3 service not configured");
                await _awsS3Service.DeleteAsync(tenantId, path, cancellationToken);
                break;
            default:
                throw new NotSupportedException($"Storage provider {_options.Provider} is not supported");
        }
    }
}
