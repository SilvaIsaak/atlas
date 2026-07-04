using System.IO;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Core.Abstractions;

public interface IColdStorageService
{
    Task UploadAsync(TenantId tenantId, string path, Stream content, CancellationToken cancellationToken = default);
    Task<Stream?> DownloadAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
    Task DeleteAsync(TenantId tenantId, string path, CancellationToken cancellationToken = default);
}
