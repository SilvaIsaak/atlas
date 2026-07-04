using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;
using CryptoAIPlatform.Domain.Risk.ValueObjects;

namespace CryptoAIPlatform.Domain.Risk;

public class ExposureProfile : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public DateTime GeneratedAt { get; private set; }
    public List<ExposureItem> Items { get; private set; } = new();
    public MaxExposure TotalExposure { get; private set; } = null!;
    public ConcentrationRisk? HighestConcentration { get; private set; }

    private ExposureProfile() { }

    public static ExposureProfile Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        DateTime generatedAt,
        MaxExposure totalExposure,
        ConcentrationRisk? highestConcentration,
        Guid? createdBy = null)
    {
        return new ExposureProfile
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            GeneratedAt = generatedAt,
            TotalExposure = totalExposure,
            HighestConcentration = highestConcentration,
            CreatedBy = createdBy
        };
    }

    public void AddItem(ExposureItem item) => Items.Add(item);
}

public class ExposureItem : BaseEntity<Guid>
{
    public Guid ProfileId { get; private set; }
    public string Symbol { get; private set; } = string.Empty;
    public ExposureType Type { get; private set; }
    public decimal Size { get; private set; }
    public decimal NotionalValue { get; private set; }
    public decimal ConcentrationPercentage { get; private set; }

    private ExposureItem() { }

    public static ExposureItem Create(
        Guid id,
        TenantId tenantId,
        Guid profileId,
        string symbol,
        ExposureType type,
        decimal size,
        decimal notionalValue,
        decimal concentrationPercentage,
        Guid? createdBy = null)
    {
        return new ExposureItem
        {
            Id = id,
            TenantId = tenantId,
            ProfileId = profileId,
            Symbol = symbol,
            Type = type,
            Size = size,
            NotionalValue = notionalValue,
            ConcentrationPercentage = concentrationPercentage,
            CreatedBy = createdBy
        };
    }
}
