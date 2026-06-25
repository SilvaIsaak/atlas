using MediatR;
using CryptoAIPlatform.Domain.Notifications;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Notifications;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateNotificationCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateNotificationResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Type = request.Type,
            Title = request.Title,
            Message = request.Message,
            IsRead = false
        };

        await _dbContext.Notifications.AddAsync(notification, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateNotificationResponse
        {
            NotificationId = notification.Id,
            IsRead = notification.IsRead
        };
    }
}