namespace CryptoAIPlatform.Domain.Exchanges;

public interface IExchangeAuthenticationService
{
    Task<bool> ValidateCredentialsAsync(CancellationToken cancellationToken = default);
}
