namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

public record EnvironmentInfo(
    string DotNetVersion,
    string? PackageVersions = null,
    string? OsVersion = null,
    string? DockerImageTag = null);