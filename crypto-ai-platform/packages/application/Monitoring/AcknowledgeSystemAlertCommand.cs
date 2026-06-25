using MediatR;

namespace CryptoAIPlatform.Application.Monitoring;

public record AcknowledgeSystemAlertCommand : IRequest<AcknowledgeSystemAlertResponse>
{
    public Guid AlertId { get; init; }
}

public record AcknowledgeSystemAlertResponse
{
    public Guid AlertId { get; init; }
    public bool Acknowledged { get; init; }
}
