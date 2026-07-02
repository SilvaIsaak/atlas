using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public class Experiment : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<ExperimentParameter> _parameters = [];
    private readonly List<ExperimentRun> _runs = [];

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ExperimentType Type { get; private set; }
    public Guid OwnerId { get; private set; }

    public IReadOnlyCollection<ExperimentParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyCollection<ExperimentRun> Runs => _runs.AsReadOnly();

    private Experiment() { }

    public static Experiment Create(
        Guid id,
        TenantId tenantId,
        string name,
        string description,
        ExperimentType type,
        Guid ownerId,
        Guid? createdBy = null)
    {
        return new Experiment
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            Description = description,
            Type = type,
            OwnerId = ownerId,
            CreatedBy = createdBy
        };
    }

    public void Update(string? name = null, string? description = null, Guid? updatedBy = null)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void AddParameter(ExperimentParameter parameter)
    {
        _parameters.Add(parameter);
    }

    public void AddRun(ExperimentRun run)
    {
        _runs.Add(run);
    }
}