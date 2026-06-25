using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Exchanges;

public class Exchange : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Ex: "BINANCE", "COINBASE"
    public string ApiBaseUrl { get; set; } = string.Empty;
    public string WsUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public List<ExchangeIntegration> Integrations { get; set; } = new List<ExchangeIntegration>();
}

public class ExchangeIntegration : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ExchangeId { get; set; }
    public Exchange? Exchange { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
    public string? Passphrase { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}