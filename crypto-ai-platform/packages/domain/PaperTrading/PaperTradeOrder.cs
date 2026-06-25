namespace CryptoAIPlatform.Domain.PaperTrading;

public record PaperTradeOrder(
    Guid OrderId,
    string Symbol,
    string OrderType, // Market, Limit
    string Side, // Buy, Sell
    decimal Quantity,
    decimal? Price,
    decimal? ExecutedPrice,
    DateTime CreatedAt,
    DateTime? ExecutedAt,
    string Status // Pending, Executed, Cancelled
);