using MediatR;
using Microsoft.AspNetCore.Identity;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null) throw new InvalidOperationException("Role não encontrada.");

        var result = await _roleManager.DeleteAsync(role);
        return result.Succeeded;
    }
}
