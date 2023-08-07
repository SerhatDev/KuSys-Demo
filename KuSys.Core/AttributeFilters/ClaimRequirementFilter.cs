using System.Security.Claims;
using KuSys.Core.Constants;
using KuSys.Core.Exceptions;
using KuSys.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace KuSys.Core.AttributeFilters;

/// <summary>
/// Attribute for specifying Claim requirements while Authorization.
/// </summary>
public class ClaimRequirementAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="claimType">Type of the claim (US-1,US-2,US-3,US-4)</param>
    /// <param name="permissionType"></param>
    public ClaimRequirementAttribute(string claimType, PermissionType permissionType = PermissionType.FullAccess)
        : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, permissionType.ToString()) };
    }
}

/// <summary>
/// Authorization filter for defining claim reuqirements.
/// </summary>
public class 
    ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="claim"></param>
    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    /// <summary>
    /// Process to check if user has required claims for the operation.
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Find user role
        var userRole = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        
        // Check if user has any roles
        if(string.IsNullOrEmpty(userRole))
            throw new AuthenticationException("You are not authorized for that operation");

        if (userRole == DefaultRoles.Student && context.HttpContext.User.FindAll(ClaimTypes.NameIdentifier).Any(x=> x.Value == context.HttpContext.GetRouteValue("id").ToString()))
        {
            
        }
        else
        {
            // Get roleManager through ServiceProvider
            var roleManager = context.HttpContext.RequestServices.GetRequiredService<RoleManager<UserRole>>();
        
            // Get role object of the user
            var role = roleManager.Roles.FirstOrDefault(x => x.Name == userRole);
        
            // Check to see if user's role has required claims to access to data
            // User should have the ClaimType required
            // Also the value of the Claim should be either 'FullAccess' or should match the required permissionType
            var hasPermission = roleManager.GetClaimsAsync(role)
                .Result
                .Any(claim => claim.Type == _claim.Type
                              && (claim.Value == PermissionType.FullAccess.ToString() || _claim.Type == claim.Type));

            // If user role isn't allowed to see the content, throw Authorization error
            if (!hasPermission)
                throw new AuthenticationException("You are not authorized for that operation");
        }
    }
}