using EShop.Infrastructure.Configuration;
using EShop.Models.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Initialization
{
    public static class AppStart
    {
        public static async Task Run(IServiceProvider provider)
        {
            await InitializeUsersAndRoles(provider);
        }

        private static async Task InitializeUsersAndRoles(IServiceProvider provider)
        {
            try
            {
                var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
                var rolesManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                var superUser = provider.GetRequiredService<IOptions<SuperUser>>();

                await EnsureUsersAndRoles(userManager, rolesManager, superUser.Value);
            }
            catch (Exception ex)
            {
                var logger = provider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }



            static async Task EnsureUsersAndRoles(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SuperUser superUser)
            {
                await EnsureRoles(roleManager);
                await EnsureSuperUser(userManager, superUser);
            }

            static async Task EnsureRoles(RoleManager<IdentityRole> roleManager)
            {
                var roles = new[] { Role.SuperUser, Role.Admin, Role.Director, Role.User };

                foreach (string role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            static async Task EnsureSuperUser(UserManager<IdentityUser> userManager, SuperUser superUser)
            {
                IdentityUser user = await userManager.FindByEmailAsync(superUser.Email);
                if (user != null)
                {
                    await userManager.DeleteAsync(user);
                }

                user = new IdentityUser
                {
                    UserName = superUser.Email,
                    Email = superUser.Email,
                };

                IdentityResult result = await userManager.CreateAsync(user, superUser.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.SuperUser);
                }
            }
        }
    }
}
