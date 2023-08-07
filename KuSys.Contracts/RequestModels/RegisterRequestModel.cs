using KuSys.Core;

namespace KuSys.Contracts.RequestModels;

/// <summary>
/// Model to register new user.
/// </summary>
public sealed class RegisterRequestModel
    : IBaseRequest
{
    /// <summary>
    /// UserName. This should be unique.
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Email Address. This should be Unique. 
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Last name
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Birth date.
    /// </summary>
    public DateTime BirthDate { get; set; }
    
    /// <summary>
    /// Gender. 0 = WontProvide, 1 = Male, 2 = Female
    /// </summary>
    public Gender Gender { get; set; }
}