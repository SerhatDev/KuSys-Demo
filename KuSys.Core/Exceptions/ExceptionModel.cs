using System.Net;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Exception model.
/// </summary>
public sealed class ExceptionModel
{
    /// <summary>
    /// Http Status Code
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; init; }
    
    /// <summary>
    /// Custom Error Code for the exception
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Exception Message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// List of errors for the exception
    /// </summary>
    public List<string> Errors { get; set; }

    /// <summary>
    /// Additional information about the exception
    /// </summary>
    public string Description { get; set; }

}