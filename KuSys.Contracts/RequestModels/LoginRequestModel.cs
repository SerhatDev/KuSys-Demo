namespace KuSys.Contracts.RequestModels;

/// <summary>
/// Request model to login into system.
/// </summary>
public sealed class LoginRequestModel
    : IBaseRequest
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