using KuSys.Core.Constants;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Authentication exception type
/// </summary>
public sealed class AuthenticationException : BaseException
{
    /// <summary>
    /// Create Authentication exception with default error message.
    /// </summary>
    public AuthenticationException() 
        : base(ErrorCodes.AuthenticationExceptionErrorCode, ErrorCodes.AuthenticationExceptionDefaultMsg)
    {
    }
    
    /// <summary>
    /// Create Authentication exception with provided message
    /// </summary>
    /// <param name="message"></param>
    public AuthenticationException(string message) 
        : base(ErrorCodes.AuthenticationExceptionErrorCode, message)
    {
    }
}