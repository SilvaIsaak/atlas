using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Services.Storage;

public interface IAwsS3StorageService
{
    Task UploadAsync(TenantId tenantId, string path, Stream content, CancellationToken cancellationToken = default);
    Task<Stream?> DownloadAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
    Task DeleteAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
}
