namespace KuSys.Core;

/// <summary>
/// Definiton of PermissionTypes.
/// </summary>
public enum PermissionType
{
    /// <summary>
    /// Can do the operation only for own user.
    /// </summary>
    Self,
    
    /// <summary>
    /// Can do the operation for everyone.
    /// </summary>
    All
}