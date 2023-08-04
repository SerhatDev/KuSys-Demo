namespace KuSys.Core.Types;

/// <summary>
/// Response type for login operations.
/// </summary>
public sealed class LoginResponse
{
    /// <summary>
    /// Access token.
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// Status of operation.
    /// </summary>
    public bool IsSuccessfull { get; set; }
    
    /// <summary>
    /// Error message if the login operation was failed.
    /// </summary>
    public string ErrorMessage { get; set; }
}