using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Notifications;

public class Notification : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
}