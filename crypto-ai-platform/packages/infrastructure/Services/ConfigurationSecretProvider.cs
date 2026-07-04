using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.Abstractions;

namespace CryptoAIPlatform.Infrastructure.Services;

public class ConfigurationSecretProvider : ISecretProvider
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ConfigurationSecretProvider> _logger;

    public ConfigurationSecretProvider(IConfiguration configuration, ILogger<ConfigurationSecretProvider> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public Task<string> GetSecretAsync(string secretName, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving secret {SecretName} from configuration", secretName);
        var secretValue = _configuration[secretName];
        if (string.IsNullOrEmpty(secretValue))
        {
            throw new KeyNotFoundException($"Secret {secretName} not found in configuration");
        }
        return Task.FromResult(secretValue);
    }

    public Task SetSecretAsync(string secretName, string secretValue, CancellationToken cancellationToken = default)
    {
        _logger.LogWarning("SetSecretAsync not implemented for ConfigurationSecretProvider; use Azure Key Vault or AWS Secrets Manager in production");
        return Task.CompletedTask;
    }
}
