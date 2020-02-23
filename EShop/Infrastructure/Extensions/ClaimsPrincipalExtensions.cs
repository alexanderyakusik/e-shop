using System.Security.Claims;
using EShop.Models.App;

namespace EShop.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsSuperUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(Role.SuperUser);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(Role.Admin);
        }

        public static bool IsDirector(this ClaimsPrincipal user)
        {
            return user.IsInRole(Role.Director);
        }

        public static bool IsUser(this ClaimsPrincipal user)
        {
            return user.IsInRole(Role.User);
        }
    }
}
