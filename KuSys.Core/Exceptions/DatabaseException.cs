using KuSys.Core.Constants;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Type for database exceptions.
/// </summary>
public sealed class DatabaseException : BaseException
{
    /// <summary>
    /// Create database exception object with default error message.
    /// </summary>
    public DatabaseException() 
        : base(ErrorCodes.DatabaseExceptionErrorCode, ErrorCodes.DatabaseExceptionDefaultMsg)
    {
    }
    
    /// <summary>
    /// Create database exception object with provided error message.
    /// </summary>
    /// <param name="message"></param>
    public DatabaseException(string message) 
        : base(ErrorCodes.DatabaseExceptionErrorCode, message)
    {
    }
}