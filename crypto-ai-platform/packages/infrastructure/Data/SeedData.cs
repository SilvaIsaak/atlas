using CryptoAIPlatform.Domain.IdentityAndAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoAIPlatform.Infrastructure.Data;

public static class SeedData
{
    public static async Task SeedDemoDataAsync(
        ApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        // Stub for seed data (will implement with actual constructors when needed)
        await Task.CompletedTask;
    }
}
