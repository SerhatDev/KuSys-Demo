using System.Net;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Base class of Custom Exception Types
/// </summary>
public abstract class BaseException : System.Exception
{
    /// <summary>
    /// Status code of the exception
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; init; }
    
    /// <summary>
    /// Custom Error Code for the exception
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Exception Message
    /// </summary>
    public new string Message { get; set; }

    /// <summary>
    /// List of errors for the exception
    /// </summary>
    public List<string> Errors { get; set; }

    /// <summary>
    /// Additional information about the exception
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Base class of Exceptions
    /// </summary>
    /// <param name="errorCode">Error Code</param>
    /// <param name="message">Error Messages</param>
    protected BaseException(string errorCode, string message)
    {
        this.Message = message;
        this.ErrorCode = errorCode;
    }
}