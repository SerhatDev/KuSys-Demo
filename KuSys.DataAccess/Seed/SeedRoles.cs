using KuSys.Core.Constants;
using KuSys.Entities;
using Microsoft.AspNetCore.Identity;

namespace KuSys.DataAccess.Seed;

/// <summary>
/// Type to seed roles, we couldnt use DbContext's modelbuilder since we are using AspNet.Identity package.
/// </summary>
public class SeedRoles
{
    private readonly RoleManager<UserRole> _userRoleManager;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="userRoleManager"></param>
    public SeedRoles(RoleManager<UserRole> userRoleManager)
    {
        _userRoleManager = userRoleManager;
    }

    /// <summary>
    /// Seed Default roles.
    /// </summary>
    /// <returns></returns>
    public void Seed()
    {
        // If roles has been saved into database before, Do not add any roles
        if (_userRoleManager.Roles.Count(x => true) > 0)
            return;
        
        // Admin Role
        var adminRole = new UserRole()
        {
            Name = DefaultRoles.Admin
        };
        
        // Student Role
        var studentRole = new UserRole()
        {
            Name = DefaultRoles.Student
        };

        // We have to wait this task because we can not do async operations in single DbContext
        _userRoleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

        // We have to wait this task because we can not do async operations in single DbContext
        _userRoleManager.CreateAsync(studentRole).GetAwaiter().GetResult();

    }
}