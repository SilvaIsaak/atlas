using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class MarketDataSource : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<MarketDataIngestionJob> _ingestionJobs = [];
    private readonly List<MarketDataAsset> _assets = [];

    public string Name { get; private set; } = string.Empty;
    public string BaseUrl { get; private set; } = string.Empty;
    public string? EncryptedApiKey { get; private set; }
    public string? ApiKeyNonce { get; private set; }
    public string? ApiKeyTag { get; private set; }
    public bool IsActive { get; private set; }
    public MarketDataSourceType Type { get; private set; }

    public IReadOnlyCollection<MarketDataIngestionJob> IngestionJobs => _ingestionJobs.AsReadOnly();
    public IReadOnlyCollection<MarketDataAsset> Assets => _assets.AsReadOnly();

    private MarketDataSource() { }

    public static MarketDataSource Create(
        Guid id,
        TenantId tenantId,
        string name,
        string baseUrl,
        MarketDataSourceType type,
        bool isActive = true,
        string? encryptedApiKey = null,
        string? apiKeyNonce = null,
        string? apiKeyTag = null,
        Guid? createdBy = null)
    {
        var dataSource = new MarketDataSource
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            BaseUrl = baseUrl,
            Type = type,
            IsActive = isActive,
            EncryptedApiKey = encryptedApiKey,
            ApiKeyNonce = apiKeyNonce,
            ApiKeyTag = apiKeyTag,
            CreatedBy = createdBy
        };

        return dataSource;
    }

    public void Update(
        string? name = null,
        string? baseUrl = null,
        bool? isActive = null,
        string? encryptedApiKey = null,
        string? apiKeyNonce = null,
        string? apiKeyTag = null,
        Guid? updatedBy = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(baseUrl))
            BaseUrl = baseUrl;
        if (isActive.HasValue)
            IsActive = isActive.Value;
        if (!string.IsNullOrWhiteSpace(encryptedApiKey))
            EncryptedApiKey = encryptedApiKey;
        if (!string.IsNullOrWhiteSpace(apiKeyNonce))
            ApiKeyNonce = apiKeyNonce;
        if (!string.IsNullOrWhiteSpace(apiKeyTag))
            ApiKeyTag = apiKeyTag;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void AddIngestionJob(MarketDataIngestionJob job)
    {
        _ingestionJobs.Add(job);
    }

    public void AddAsset(MarketDataAsset asset)
    {
        _assets.Add(asset);
    }
}