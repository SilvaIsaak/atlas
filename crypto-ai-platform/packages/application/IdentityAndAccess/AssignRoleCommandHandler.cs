using MediatR;
using Microsoft.AspNetCore.Identity;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, AssignRoleResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AssignRoleCommandHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AssignRoleResponse> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new InvalidOperationException("User not found.");

        var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
        if (!roleExists) throw new InvalidOperationException("Role not found.");

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);
        return new AssignRoleResponse
        {
            UserId = request.UserId,
            RoleName = request.RoleName,
            Success = result.Succeeded
        };
    }
}