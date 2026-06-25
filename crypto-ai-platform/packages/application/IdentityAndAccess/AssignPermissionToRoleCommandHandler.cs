using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class AssignPermissionToRoleCommandHandler : IRequestHandler<AssignPermissionToRoleCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;

    public AssignPermissionToRoleCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
        
        if (role == null)
            throw new InvalidOperationException("Role não encontrada.");

        if (role.RolePermissions.Any(rp => rp.Permission == request.Permission))
            return true;

        role.RolePermissions.Add(new RolePermission
        {
            RoleId = request.RoleId,
            Permission = request.Permission
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
