namespace CryptoAIPlatform.Domain.Core.ValueObjects;

public record CorrelationId(Guid Value)
{
    public static CorrelationId New() => new(Guid.NewGuid());
    public static CorrelationId From(Guid value) => new(value);
    public static CorrelationId From(string value) => new(Guid.Parse(value));
}
