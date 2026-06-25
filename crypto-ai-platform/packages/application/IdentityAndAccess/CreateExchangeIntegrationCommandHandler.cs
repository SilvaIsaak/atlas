using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class CreateExchangeIntegrationCommandHandler : IRequestHandler<CreateExchangeIntegrationCommand, ExchangeIntegrationDto>
{
    private readonly ApplicationDbContext _context;

    public CreateExchangeIntegrationCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExchangeIntegrationDto> Handle(CreateExchangeIntegrationCommand request, CancellationToken cancellationToken)
    {
        var exchange = await _context.Exchanges.FirstOrDefaultAsync(e => e.Id == request.ExchangeId, cancellationToken);
        if (exchange == null) throw new InvalidOperationException("Exchange not found");

        var integration = new ExchangeIntegration
        {
            UserId = request.UserId,
            ExchangeId = request.ExchangeId,
            ApiKey = request.ApiKey,
            ApiSecret = request.ApiSecret,
            Passphrase = request.Passphrase
        };

        _context.ExchangeIntegrations.Add(integration);
        await _context.SaveChangesAsync(cancellationToken);

        return new ExchangeIntegrationDto
        {
            Id = integration.Id,
            UserId = integration.UserId,
            ExchangeId = integration.ExchangeId,
            ExchangeName = exchange.Name,
            IsActive = integration.IsActive,
            CreatedAt = integration.CreatedAt
        };
    }
}
