using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Mobile;

public class MobileDevice : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public MobilePlatform Platform { get; set; }
    public string? DeviceToken { get; set; }
    public string? DeviceModel { get; set; }
    public string? OsVersion { get; set; }
    public string? AppVersion { get; set; }
    public bool NotificationsEnabled { get; set; }
    public DateTime LastActiveAt { get; set; }
}
