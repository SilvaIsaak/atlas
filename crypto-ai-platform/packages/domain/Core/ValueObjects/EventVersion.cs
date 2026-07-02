namespace CryptoAIPlatform.Domain.Core.ValueObjects;

public record EventVersion(int Major, int Minor, int Patch)
{
    public static EventVersion V1_0_0 = new(1, 0, 0);
    public override string ToString() => $"{Major}.{Minor}.{Patch}";
}
