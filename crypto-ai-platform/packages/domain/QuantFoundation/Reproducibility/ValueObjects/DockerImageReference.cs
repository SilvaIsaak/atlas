namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;

public record DockerImageReference(string ImageName, string Tag, string? Digest = null);
