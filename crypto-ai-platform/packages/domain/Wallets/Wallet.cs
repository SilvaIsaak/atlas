using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Domain.Wallets;

public class Wallet : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid? ExchangeIntegrationId { get; set; } // If null, it's the platform's internal wallet
    public List<WalletBalance> Balances { get; set; } = new List<WalletBalance>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class WalletBalance : BaseEntity<Guid>
{
    public Guid WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public string Asset { get; set; } = string.Empty; // e.g., "BTC", "ETH", "USDT"
    public decimal Free { get; set; } // Available balance
    public decimal Locked { get; set; } // Locked in orders, etc.
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
