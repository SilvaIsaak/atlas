namespace CryptoAIPlatform.Domain.Core.ValueObjects;

public record IdempotencyKey(Guid Value)
{
    public static IdempotencyKey New() => new(Guid.NewGuid());
    public static IdempotencyKey From(Guid value) => new(value);
    public static IdempotencyKey From(string value) => new(Guid.Parse(value));
}
