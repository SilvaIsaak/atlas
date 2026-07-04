namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

public record PythonEnvironment(
    string PythonVersion,
    Dictionary<string, string> Packages,
    string? VirtualEnvName = null);
