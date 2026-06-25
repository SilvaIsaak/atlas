using MediatR;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record CreateExchangeIntegrationCommand(Guid UserId, Guid ExchangeId, string ApiKey, string ApiSecret, string? Passphrase = null) : IRequest<ExchangeIntegrationDto>;

public record ExchangeIntegrationDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid ExchangeId { get; init; }
    public string ExchangeName { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
}
