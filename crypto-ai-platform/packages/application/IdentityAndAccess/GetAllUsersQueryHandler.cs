using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetUserByIdResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetAllUsersQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<GetUserByIdResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        var userResponses = new List<GetUserByIdResponse>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userResponses.Add(new GetUserByIdResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList()
            });
        }

        return userResponses;
    }
}
