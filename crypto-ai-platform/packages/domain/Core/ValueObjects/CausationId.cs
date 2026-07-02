namespace CryptoAIPlatform.Domain.Core.ValueObjects;

public record CausationId(Guid Value)
{
    public static CausationId New() => new(Guid.NewGuid());
    public static CausationId From(Guid value) => new(value);
    public static CausationId From(string value) => new(Guid.Parse(value));
}
