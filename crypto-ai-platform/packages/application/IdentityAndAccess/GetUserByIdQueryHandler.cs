using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly UserManager<User> _userManager;

    public GetUserByIdQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) throw new InvalidOperationException("Usuário não encontrado.");

        var roles = await _userManager.GetRolesAsync(user);

        return new GetUserByIdResponse
        {
            UserId = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles.ToList()
        };
    }
}
