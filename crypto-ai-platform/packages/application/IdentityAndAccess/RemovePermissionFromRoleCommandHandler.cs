using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class RemovePermissionFromRoleCommandHandler : IRequestHandler<RemovePermissionFromRoleCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public RemovePermissionFromRoleCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(RemovePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = await _dbContext.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == request.RoleId && rp.Permission == request.Permission, cancellationToken);

        if (rolePermission == null)
            return true;

        _dbContext.RolePermissions.Remove(rolePermission);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
