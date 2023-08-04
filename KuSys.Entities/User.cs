using System.ComponentModel.DataAnnotations;
using KuSys.Core;
using Microsoft.AspNetCore.Identity;

namespace KuSys.Entities;

/// <summary>
/// User entity. Derived from IdentityUser to Use AspNet.Identity package
/// </summary>
public sealed class User  : IdentityUser<Guid>
{
    /// <summary>
    /// First name of the user
    /// </summary>
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last name of the user
    /// </summary>
    [MaxLength(50)]
    public string LastName { get; set; }
    
    /// <summary>
    /// Birth date of the user
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Used to soft-delete users. 
    /// </summary>
    public bool IsDeleted { get; set; }
}