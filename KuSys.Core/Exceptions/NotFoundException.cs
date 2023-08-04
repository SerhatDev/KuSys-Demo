using KuSys.Core.Constants;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Type for not found data exceptions.
/// </summary>
public sealed class DataNotFoundException : BaseException
{
    /// <summary>
    /// Create not found exception with default error message.
    /// </summary>
    public DataNotFoundException() 
        : base(ErrorCodes.DatabaseExceptionErrorCode, ErrorCodes.DatabaseExceptionDefaultMsg)
    {
    }
    
    /// <summary>
    /// Create not found exception with provided error message.
    /// </summary>
    /// <param name="message">Error message</param>
    public DataNotFoundException(string message)
        : base(ErrorCodes.DatabaseExceptionErrorCode, message)
    {
        
    }
}