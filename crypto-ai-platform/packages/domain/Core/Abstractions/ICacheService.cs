using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Core.Abstractions;

public interface ICacheService
{
    Task<T?> GetAsync<T>(TenantId tenantId, string key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(TenantId tenantId, string key, T value, TimeSpan? ttl = null, CancellationToken cancellationToken = default);
    Task RemoveAsync(TenantId tenantId, string key, CancellationToken cancellationToken = default);
    Task RemoveByPrefixAsync(TenantId tenantId, string prefix, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TenantId tenantId, string key, CancellationToken cancellationToken = default);
}
