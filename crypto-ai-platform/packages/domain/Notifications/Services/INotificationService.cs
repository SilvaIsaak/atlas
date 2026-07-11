using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Notifications.Services;

public interface INotificationService
{
    Task SendEmailAsync(TenantId tenantId, string to, string subject, string body, CancellationToken cancellationToken = default);
    Task SendTelegramAsync(TenantId tenantId, string chatId, string message, CancellationToken cancellationToken = default);
    Task SendDiscordAsync(TenantId tenantId, string webhookUrl, string message, CancellationToken cancellationToken = default);
    Task SendSlackAsync(TenantId tenantId, string webhookUrl, string message, CancellationToken cancellationToken = default);
    Task SendWebhookAsync(TenantId tenantId, string webhookUrl, object payload, CancellationToken cancellationToken = default);
    Task SendPushAsync(TenantId tenantId, string deviceToken, string title, string body, CancellationToken cancellationToken = default);
}

public interface INotificationRepository { }
