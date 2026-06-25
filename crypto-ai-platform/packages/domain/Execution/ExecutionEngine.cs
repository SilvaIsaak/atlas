using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Domain.Execution;

public class ExecutionEngine : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid ExchangeIntegrationId { get; set; }
    public ExchangeIntegration? ExchangeIntegration { get; set; }
    public List<ExecutionOrder>? Orders { get; set; }
    public bool IsActive { get; set; }
}