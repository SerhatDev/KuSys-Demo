using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace KuSys.Core.Constants;

/// <summary>
/// Definition of Default Roles. These roles will be added into Database if they dont exist.
/// </summary>
public static class DefaultRoles
{
    /// <summary>
    /// Admin Role Constant
    /// </summary>
    public const string Admin = "Admin";
    
    /// <summary>
    /// Student Role Constant
    /// </summary>
    public const string Student = "Student";

    /// <summary>
    /// Get default claims for Admin Role.
    /// </summary>
    /// <returns></returns>
    public static List<IdentityRoleClaim<Guid>> AdminClaims(Guid roleId)
    {
        List<IdentityRoleClaim<Guid>> claims = new();
        
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-1", 
            ClaimValue = PermissionType.FullAccess.ToString(),
            RoleId = roleId,
            Id = 1
        });
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-2", 
            ClaimValue = PermissionType.FullAccess.ToString(),
            RoleId = roleId,
            Id = 2
        });
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-3", 
            ClaimValue = PermissionType.FullAccess.ToString(),
            RoleId = roleId,
            Id = 3
        });
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-4", 
            ClaimValue = PermissionType.FullAccess.ToString(),
            RoleId = roleId,
            Id = 4
        });

        return claims;
    }

    /// <summary>
    /// Get default claims for User role.
    /// </summary>
    /// <returns></returns>
    public static List<IdentityRoleClaim<Guid>> StudentClaims(Guid roleId)
    {
        List<IdentityRoleClaim<Guid>> claims = new();
        
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-2", 
            ClaimValue = PermissionType.StudentAccess.ToString(),
            RoleId = roleId,
            Id = 5
        });
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-3", 
            ClaimValue = PermissionType.StudentAccess.ToString(),
            RoleId = roleId,
            Id = 6
        });
        claims.Add(new IdentityRoleClaim<Guid>()
        {
            ClaimType = "US-4", 
            ClaimValue = PermissionType.FullAccess.ToString(),
            RoleId = roleId,
            Id = 7
        });

        return claims;
    }
} 