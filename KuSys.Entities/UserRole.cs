using Microsoft.AspNetCore.Identity;

namespace KuSys.Entities;

/// <summary>
/// User role entity. Derived from IndetityRole to use AspNet.Identity package
/// </summary>
public sealed class UserRole : IdentityRole<Guid>
{
    
}