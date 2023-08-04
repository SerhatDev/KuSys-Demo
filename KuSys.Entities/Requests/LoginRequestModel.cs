namespace KuSys.Entities.Requests;

/// <summary>
/// Request model to login into system.
/// </summary>
public sealed class LoginRequestModel
{
    /// <summary>
    /// Email address of the user.
    /// </summary>
    public string Email { get; set; }
    
    
    /// <summary>
    /// Password of the user.
    /// </summary>
    public string Password { get; set; }
}