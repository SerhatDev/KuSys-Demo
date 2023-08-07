namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Response type for login operations.
/// </summary>
public sealed class LoginResponseModel
    : BaseResponse
{
    /// <summary>
    /// Access token.
    /// </summary>
    public string Token { get; set; }
}