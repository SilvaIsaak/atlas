namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

public record DotNetEnvironment(
    string DotNetSdkVersion,
    string TargetFramework,
    Dictionary<string, string> Packages);
