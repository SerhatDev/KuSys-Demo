using System.Security.Claims;

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
    public static List<Claim> AdminClaims()
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("US-1",PermissionType.All.ToString()));
        claims.Add(new Claim("US-2",PermissionType.All.ToString()));
        claims.Add(new Claim("US-3",PermissionType.All.ToString()));
        claims.Add(new Claim("US-4",PermissionType.All.ToString()));

        return claims;
    }

    /// <summary>
    /// Get default claims for User role.
    /// </summary>
    /// <returns></returns>
    public static List<Claim> StudentClaims()
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("US-2",PermissionType.Self.ToString()));
        claims.Add(new Claim("US-3",PermissionType.Self.ToString()));
        claims.Add(new Claim("US-4",PermissionType.Self.ToString()));

        return claims;
    }
} 