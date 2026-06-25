using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly ApplicationDbContext _dbContext;

    public CreateRoleCommandHandler(RoleManager<Role> roleManager, ApplicationDbContext dbContext)
    {
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = request.Name,
            Description = request.Description
        };

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create role: {errors}");
        }

        if (request.Permissions != null && request.Permissions.Any())
        {
            foreach (var permission in request.Permissions)
            {
                role.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    Permission = permission
                });
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new CreateRoleResponse
        {
            RoleId = role.Id,
            Name = role.Name!,
            Description = role.Description,
            Permissions = role.RolePermissions.Select(rp => rp.Permission).ToList()
        };
    }
}