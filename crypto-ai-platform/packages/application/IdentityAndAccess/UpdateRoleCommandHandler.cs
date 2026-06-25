using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, GetRoleByIdResponse>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _dbContext;

    public UpdateRoleCommandHandler(RoleManager<Role> roleManager, ApplicationDbContext dbContext)
    {
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<GetRoleByIdResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);
        
        if (role == null)
            throw new InvalidOperationException("Role não encontrada.");

        role.Name = request.Name;
        role.Description = request.Description;

        if (request.Permissions != null)
        {
            _dbContext.RolePermissions.RemoveRange(role.RolePermissions);
            
            foreach (var permission in request.Permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    Permission = permission
                });
            }
        }

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Falha ao atualizar role: {errors}");
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GetRoleByIdResponse(
            role.Id,
            role.Name!,
            role.Description,
            role.CreatedAt,
            role.RolePermissions.Select(rp => rp.Permission).ToList()
        );
    }
}
