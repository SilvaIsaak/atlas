using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Wallets;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Wallets;

public record GetWalletQuery(Guid UserId, Guid? ExchangeIntegrationId = null) : IRequest<WalletDto?>;

public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, WalletDto?>
{
    private readonly ApplicationDbContext _context;

    public GetWalletQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WalletDto?> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .Where(w => w.UserId == request.UserId && w.ExchangeIntegrationId == request.ExchangeIntegrationId)
            .Include(w => w.Balances)
            .Select(w => new WalletDto
            {
                Id = w.Id,
                UserId = w.UserId,
                ExchangeIntegrationId = w.ExchangeIntegrationId,
                Balances = w.Balances.Select(b => new WalletBalanceDto
                {
                    Id = b.Id,
                    Asset = b.Asset,
                    Free = b.Free,
                    Locked = b.Locked,
                    UpdatedAt = b.UpdatedAt
                }).ToList(),
                UpdatedAt = w.UpdatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return wallet;
    }
}

public record WalletDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid? ExchangeIntegrationId { get; init; }
    public List<WalletBalanceDto> Balances { get; init; } = new();
    public DateTime? UpdatedAt { get; init; }
}

public record WalletBalanceDto
{
    public Guid Id { get; init; }
    public string Asset { get; init; } = string.Empty;
    public decimal Free { get; init; }
    public decimal Locked { get; init; }
    public DateTime UpdatedAt { get; init; }
}
