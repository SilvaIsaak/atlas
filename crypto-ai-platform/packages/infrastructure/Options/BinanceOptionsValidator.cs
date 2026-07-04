using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace CryptoAIPlatform.Infrastructure.Options;

public class BinanceOptionsValidator : IValidateOptions<BinanceOptions>
{
    public ValidateOptionsResult Validate(string? name, BinanceOptions options)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(options.ApiKey))
            errors.Add("Binance ApiKey is required.");
        
        if (string.IsNullOrWhiteSpace(options.ApiSecret))
            errors.Add("Binance ApiSecret is required.");

        if (options.Spot == null)
            errors.Add("Binance Spot options are required.");
        else
        {
            if (string.IsNullOrWhiteSpace(options.Spot.ApiBaseUrl))
                errors.Add("Binance Spot ApiBaseUrl is required.");
            if (string.IsNullOrWhiteSpace(options.Spot.WsUrl))
                errors.Add("Binance Spot WsUrl is required.");
        }

        if (options.Futures == null)
            errors.Add("Binance Futures options are required.");
        else
        {
            if (string.IsNullOrWhiteSpace(options.Futures.ApiBaseUrl))
                errors.Add("Binance Futures ApiBaseUrl is required.");
            if (string.IsNullOrWhiteSpace(options.Futures.WsUrl))
                errors.Add("Binance Futures WsUrl is required.");
        }

        return errors.Count > 0 
            ? ValidateOptionsResult.Fail(errors) 
            : ValidateOptionsResult.Success;
    }
}

public class ExchangeOptionsValidator : IValidateOptions<ExchangeOptions>
{
    public ValidateOptionsResult Validate(string? name, ExchangeOptions options)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(options.DefaultExchange))
            errors.Add("DefaultExchange is required.");

        return errors.Count > 0 
            ? ValidateOptionsResult.Fail(errors) 
            : ValidateOptionsResult.Success;
    }
}

public class RateLimitOptionsValidator : IValidateOptions<RateLimitOptions>
{
    public ValidateOptionsResult Validate(string? name, RateLimitOptions options)
    {
        var errors = new List<string>();

        if (options.RequestLimitPerSecond <= 0)
            errors.Add("RequestLimitPerSecond must be greater than 0.");
        if (options.RequestLimitPerMinute <= 0)
            errors.Add("RequestLimitPerMinute must be greater than 0.");
        if (options.OrderLimitPerSecond <= 0)
            errors.Add("OrderLimitPerSecond must be greater than 0.");
        if (options.OrderLimitPerDay <= 0)
            errors.Add("OrderLimitPerDay must be greater than 0.");

        return errors.Count > 0 
            ? ValidateOptionsResult.Fail(errors) 
            : ValidateOptionsResult.Success;
    }
}

public class ReconnectOptionsValidator : IValidateOptions<ReconnectOptions>
{
    public ValidateOptionsResult Validate(string? name, ReconnectOptions options)
    {
        var errors = new List<string>();

        if (options.MaxReconnectAttempts <= 0)
            errors.Add("MaxReconnectAttempts must be greater than 0.");
        if (options.InitialDelayMs <= 0)
            errors.Add("InitialDelayMs must be greater than 0.");
        if (options.MaxDelayMs <= 0)
            errors.Add("MaxDelayMs must be greater than 0.");
        if (options.InitialDelayMs > options.MaxDelayMs)
            errors.Add("InitialDelayMs cannot be larger than MaxDelayMs.");
        if (options.HeartbeatIntervalMs <= 0)
            errors.Add("HeartbeatIntervalMs must be greater than 0.");
        if (options.HeartbeatTimeoutMs <= 0)
            errors.Add("HeartbeatTimeoutMs must be greater than 0.");

        return errors.Count > 0 
            ? ValidateOptionsResult.Fail(errors) 
            : ValidateOptionsResult.Success;
    }
}
