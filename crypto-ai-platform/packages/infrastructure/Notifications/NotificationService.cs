using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Notifications.Services;

namespace CryptoAIPlatform.Infrastructure.Notifications;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(TenantId tenantId, string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending email to {To} with subject {Subject} for tenant {TenantId}", to, subject, tenantId);
        return Task.CompletedTask;
    }

    public Task SendTelegramAsync(TenantId tenantId, string chatId, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending Telegram message to chat {ChatId} for tenant {TenantId}", chatId, tenantId);
        return Task.CompletedTask;
    }

    public Task SendDiscordAsync(TenantId tenantId, string webhookUrl, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending Discord message to {WebhookUrl} for tenant {TenantId}", webhookUrl, tenantId);
        return Task.CompletedTask;
    }

    public Task SendSlackAsync(TenantId tenantId, string webhookUrl, string message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending Slack message to {WebhookUrl} for tenant {TenantId}", webhookUrl, tenantId);
        return Task.CompletedTask;
    }

    public Task SendWebhookAsync(TenantId tenantId, string webhookUrl, object payload, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending webhook to {WebhookUrl} for tenant {TenantId}", webhookUrl, tenantId);
        return Task.CompletedTask;
    }

    public Task SendPushAsync(TenantId tenantId, string deviceToken, string title, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending push notification to device {DeviceToken} for tenant {TenantId}", deviceToken, tenantId);
        return Task.CompletedTask;
    }
}
