using System.Net;
using System.Text.Json;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Exception response model
/// </summary>
public class ExceptionBuilder
{
    private ExceptionModel  _exception = new();

    /// <summary>
    /// Reset values of builder.
    /// </summary>
    /// <returns></returns>
    public ExceptionBuilder Reset()
    {
        _exception = new ExceptionModel ();
        return this;
    }

    /// <summary>
    /// Exception with specified message.
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns><see cref="ExceptionBuilder"/></returns>
    public ExceptionBuilder WithMessage(string message)
    {
        _exception.Message = message;
        return this;
    }

    /// <summary>
    /// Exception with specified list of errors.
    /// </summary>
    /// <param name="errors">List of errors</param>
    /// <returns><see cref="ExceptionBuilder"/></returns>
    public ExceptionBuilder WithErrors(List<string> errors)
    {
        _exception.Errors =errors;
        return this;
    }

    /// <summary>
    /// Exception with specified error code.
    /// </summary>
    /// <param name="errorCode">Error code</param>
    /// <returns><see cref="ExceptionBuilder"/></returns>
    public ExceptionBuilder WithErrorCode(string errorCode)
    {
        _exception.ErrorCode = errorCode;
        return this;
    }

    /// <summary>
    /// Build and get Exception object.
    /// </summary>
    /// <returns></returns>
    public ExceptionModel  Build() => _exception;

    /// <summary>
    /// Convert exception object to json.
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        return JsonSerializer.Serialize(_exception, options);
    }
}