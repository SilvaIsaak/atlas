using MediatR;
using CryptoAIPlatform.Domain.Mobile;

namespace CryptoAIPlatform.Application.Mobile;

public record RegisterMobileDeviceCommand : IRequest<RegisterMobileDeviceResponse>
{
    public Guid UserId { get; init; }
    public MobilePlatform Platform { get; init; }
    public string? DeviceToken { get; init; }
    public string? DeviceModel { get; init; }
    public string? OsVersion { get; init; }
    public string? AppVersion { get; init; }
    public bool NotificationsEnabled { get; init; }
}

public record RegisterMobileDeviceResponse
{
    public Guid DeviceId { get; init; }
    public bool NotificationsEnabled { get; init; }
}
