using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Wallets;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Infrastructure.Exchanges;

namespace CryptoAIPlatform.Application.Wallets;

public record SyncWalletBalancesCommand(Guid UserId, Guid ExchangeIntegrationId) : IRequest<WalletDto>;

public class SyncWalletBalancesCommandHandler : IRequestHandler<SyncWalletBalancesCommand, WalletDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IExchangeClientFactory _exchangeClientFactory;

    public SyncWalletBalancesCommandHandler(ApplicationDbContext context, IExchangeClientFactory exchangeClientFactory)
    {
        _context = context;
        _exchangeClientFactory = exchangeClientFactory;
    }

    public async Task<WalletDto> Handle(SyncWalletBalancesCommand request, CancellationToken cancellationToken)
    {
        var integration = await _context.ExchangeIntegrations
            .Include(ei => ei.Exchange)
            .FirstOrDefaultAsync(ei => ei.Id == request.ExchangeIntegrationId && ei.UserId == request.UserId, cancellationToken);

        if (integration == null)
        {
            throw new Exception("Exchange integration not found");
        }

        var client = _exchangeClientFactory.CreateClient(integration.Exchange!.Code, integration.ApiKey, integration.ApiSecret, integration.Passphrase);
        var balances = await client.TradingService.GetBalancesAsync(cancellationToken);

        var wallet = await _context.Wallets
            .Include(w => w.Balances)
            .FirstOrDefaultAsync(w => w.UserId == request.UserId && w.ExchangeIntegrationId == request.ExchangeIntegrationId, cancellationToken);

        if (wallet == null)
        {
            wallet = new Wallet
            {
                UserId = request.UserId,
                ExchangeIntegrationId = request.ExchangeIntegrationId,
            };
            _context.Wallets.Add(wallet);
        }

        foreach (var balance in balances)
        {
            var existingBalance = wallet.Balances.FirstOrDefault(b => b.Asset == balance.Asset);
            if (existingBalance == null)
            {
                existingBalance = new WalletBalance
                {
                    WalletId = wallet.Id,
                    Asset = balance.Asset
                };
                wallet.Balances.Add(existingBalance);
            }

            existingBalance.Free = balance.Free;
            existingBalance.Locked = balance.Locked;
            existingBalance.UpdatedAt = DateTime.UtcNow;
        }

        wallet.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);

        return new WalletDto
        {
            Id = wallet.Id,
            UserId = wallet.UserId,
            ExchangeIntegrationId = wallet.ExchangeIntegrationId,
            Balances = wallet.Balances.Select(b => new WalletBalanceDto
            {
                Id = b.Id,
                Asset = b.Asset,
                Free = b.Free,
                Locked = b.Locked,
                UpdatedAt = b.UpdatedAt
            }).ToList(),
            UpdatedAt = wallet.UpdatedAt
        };
    }
}
