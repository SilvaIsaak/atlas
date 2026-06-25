using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Domain.Mobile;

namespace CryptoAIPlatform.Application.Mobile;

public class RegisterMobileDeviceCommandHandler : IRequestHandler<RegisterMobileDeviceCommand, RegisterMobileDeviceResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public RegisterMobileDeviceCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RegisterMobileDeviceResponse> Handle(RegisterMobileDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _dbContext.MobileDevices
            .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.DeviceToken == request.DeviceToken, cancellationToken);

        if (device == null)
        {
            device = new MobileDevice
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Platform = request.Platform,
                DeviceToken = request.DeviceToken,
                DeviceModel = request.DeviceModel,
                OsVersion = request.OsVersion,
                AppVersion = request.AppVersion,
                NotificationsEnabled = request.NotificationsEnabled,
                LastActiveAt = DateTime.UtcNow
            };
            await _dbContext.MobileDevices.AddAsync(device, cancellationToken);
        }
        else
        {
            device.Platform = request.Platform;
            device.DeviceModel = request.DeviceModel;
            device.OsVersion = request.OsVersion;
            device.AppVersion = request.AppVersion;
            device.NotificationsEnabled = request.NotificationsEnabled;
            device.LastActiveAt = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RegisterMobileDeviceResponse
        {
            DeviceId = device.Id,
            NotificationsEnabled = device.NotificationsEnabled
        };
    }
}
