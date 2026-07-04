namespace CryptoAIPlatform.Domain.Core.Abstractions;

public interface ISecretProvider
{
    Task<string> GetSecretAsync(string secretName, CancellationToken cancellationToken = default);
    Task SetSecretAsync(string secretName, string secretValue, CancellationToken cancellationToken = default);
}
