using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetUserExchangeIntegrationsQuery(Guid UserId) : IRequest<List<ExchangeIntegrationDto>>;

public class GetUserExchangeIntegrationsQueryHandler : IRequestHandler<GetUserExchangeIntegrationsQuery, List<ExchangeIntegrationDto>>
{
    private readonly ApplicationDbContext _context;

    public GetUserExchangeIntegrationsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExchangeIntegrationDto>> Handle(GetUserExchangeIntegrationsQuery request, CancellationToken cancellationToken)
    {
        var integrations = await _context.ExchangeIntegrations
            .Where(ei => ei.UserId == request.UserId)
            .Include(ei => ei.Exchange)
            .Select(ei => new ExchangeIntegrationDto
            {
                Id = ei.Id,
                UserId = ei.UserId,
                ExchangeId = ei.ExchangeId,
                ExchangeName = ei.Exchange!.Name,
                IsActive = ei.IsActive,
                CreatedAt = ei.CreatedAt
            })
            .ToListAsync(cancellationToken);
        return integrations;
    }
}
