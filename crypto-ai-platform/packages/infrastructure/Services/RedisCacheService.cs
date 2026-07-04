using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Domain.Core.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly RedisOptions _options;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IDistributedCache distributedCache, IOptions<RedisOptions> options, ILogger<RedisCacheService> logger)
    {
        _distributedCache = distributedCache;
        _options = options.Value;
        _logger = logger;
    }

    private string BuildKey(TenantId tenantId, string key)
    {
        return $"tenant:{tenantId.Value.ToString()}:{key}";
    }

    public async Task<T?> GetAsync<T>(TenantId tenantId, string key, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(tenantId, key);
        try
        {
            var cachedValue = await _distributedCache.GetStringAsync(fullKey, cancellationToken);
            if (string.IsNullOrEmpty(cachedValue))
                return default;

            return JsonSerializer.Deserialize<T>(cachedValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting value from cache for key {Key}", fullKey);
            return default;
        }
    }

    public async Task SetAsync<T>(TenantId tenantId, string key, T value, TimeSpan? ttl = null, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(tenantId, key);
        try
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl ?? _options.DefaultTtl
            };

            await _distributedCache.SetStringAsync(fullKey, serializedValue, options, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting value in cache for key {Key}", fullKey);
        }
    }

    public async Task RemoveAsync(TenantId tenantId, string key, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(tenantId, key);
        try
        {
            await _distributedCache.RemoveAsync(fullKey, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing value from cache for key {Key}", fullKey);
        }
    }

    public Task RemoveByPrefixAsync(TenantId tenantId, string prefix, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("RemoveByPrefixAsync is not fully supported with IDistributedCache without specific Redis features");
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(TenantId tenantId, string key, CancellationToken cancellationToken = default)
    {
        var fullKey = BuildKey(tenantId, key);
        try
        {
            var value = await _distributedCache.GetStringAsync(fullKey, cancellationToken);
            return !string.IsNullOrEmpty(value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence in cache for key {Key}", fullKey);
            return false;
        }
    }
}
