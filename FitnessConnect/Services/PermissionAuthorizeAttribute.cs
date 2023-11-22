using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FitnessConnect.Services
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Permissions { get; set; }
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the User is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get the UserManager from the DI container
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();

            // Get the User from the UserManager
            var user = userManager.GetUserAsync(context.HttpContext.User).Result;

            // Get the RoleManager from the DI container
            var roleManager = context.HttpContext.RequestServices.GetService<RoleManager<ApplicationRole>>();

            // Check if Roles parameter is specified
            if (!string.IsNullOrEmpty(Roles))
            {

                // Split and Trim Permissions and convert it into list
                var roles = Roles.Split(',').Select(p => p.Trim()).ToList();

                // Check if the User has the specified Role
                var hasRole = context.HttpContext.RequestServices.GetService<ApplicationDBContext>()
                    .UserRoles.Any(
                        ur => ur.UserId == user.Id && roleManager.Roles.Any(
                            r => roles.Contains(r.Name) && r.Id == ur.RoleId
                        )
                    );

                if (!hasRole)
                {
                    if (!string.IsNullOrEmpty(Permissions))
                    {
                        // Split and Trim Permissions and convert it into list
                        var permissions = Permissions.Split(',').Select(p => p.Trim()).ToList();

                        // Check if the User has any Role that has the specified Permission
                        var hasPermission = context.HttpContext.RequestServices.GetService<ApplicationDBContext>()
                            .UserRoles.Any(
                                ur => ur.UserId == user.Id && roleManager.Roles.Any(
                                    r => r.Id == ur.RoleId && r.rolePermissions.Any(
                                        rp => permissions.Contains(rp.permission.Name)
                                    )
                                )
                            );

                        if (!hasPermission)
                        {
                            context.Result = new ForbidResult();
                            return;
                        }
                    }
                    else
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
            else // Role parameter is not specified
            {
                // Split and Trim Permissions and convert it into list
                var permissions = Permissions.Split(',').Select(p => p.Trim()).ToList();

                // Check if the User has any Role that has the specified Permission
                var hasPermission = context.HttpContext.RequestServices.GetService<ApplicationDBContext>()
                    .UserRoles.Any(
                        ur => ur.UserId == user.Id && roleManager.Roles.Any(
                            r => r.Id == ur.RoleId && r.rolePermissions.Any(
                                rp => permissions.Contains(rp.permission.Name)
                            )
                        )
                    );

                if (!hasPermission)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

        }
    }

    public static class AuthorizationExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permissions)
        {

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            var httpContextAccessor = new HttpContextAccessor();

            var roleManager = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<RoleManager<ApplicationRole>>();
            var dbContext = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<ApplicationDBContext>();

            var Permissions = permissions.Split(',').Select(p => p.Trim()).ToList();

            var hasPermission = dbContext.UserRoles.Any(
                ur => ur.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier) && roleManager.Roles.Any(
                    r => r.Id == ur.RoleId && r.rolePermissions.Any(rp => permissions.Contains(rp.permission.Name))
                    )
                );

            return hasPermission;
        }
    }
}
