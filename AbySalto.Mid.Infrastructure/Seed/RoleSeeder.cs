using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AbySalto.Mid.Infrastructure.Seed
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string[] roles = ["User", "Admin"];

            foreach (var roleName in roles)
            {
                var exists = await roleManager.RoleExistsAsync(roleName);
                if (!exists)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }
        }
    }
}
