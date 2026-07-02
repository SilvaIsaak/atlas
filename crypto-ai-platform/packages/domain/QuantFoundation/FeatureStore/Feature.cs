using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

public class Feature : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<FeatureVersion> _versions = [];
    private readonly List<FeatureCatalogEntry> _catalogEntries = [];

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public FeatureMaturity Maturity { get; private set; }
    public Guid OwnerId { get; private set; }
    public bool IsApproved { get; private set; }

    public IReadOnlyCollection<FeatureVersion> Versions => _versions.AsReadOnly();
    public IReadOnlyCollection<FeatureCatalogEntry> CatalogEntries => _catalogEntries.AsReadOnly();

    private Feature() { }

    public static Feature Create(
        Guid id,
        TenantId tenantId,
        string name,
        string description,
        string code,
        Guid ownerId,
        FeatureMaturity maturity = FeatureMaturity.Experimental,
        bool isApproved = false,
        Guid? createdBy = null)
    {
        return new Feature
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            Description = description,
            Code = code,
            OwnerId = ownerId,
            Maturity = maturity,
            IsApproved = isApproved,
            CreatedBy = createdBy
        };
    }

    public void Update(
        string? name = null,
        string? description = null,
        string? code = null,
        FeatureMaturity? maturity = null,
        Guid? updatedBy = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
        if (!string.IsNullOrWhiteSpace(code))
            Code = code;
        if (maturity.HasValue)
            Maturity = maturity.Value;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void Approve()
    {
        IsApproved = true;
        Maturity = FeatureMaturity.Production;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddVersion(FeatureVersion version)
    {
        _versions.Add(version);
    }

    public void AddCatalogEntry(FeatureCatalogEntry entry)
    {
        _catalogEntries.Add(entry);
    }
}