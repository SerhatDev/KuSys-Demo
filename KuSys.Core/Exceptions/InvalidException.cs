using System.Net;
using FluentValidation.Results;
using KuSys.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace KuSys.Core.Exceptions;

/// <summary>
/// Type for Validation Exceptions
/// </summary>
public sealed class InvalidException : BaseException
{
    /// <summary>
    /// Create validation exception with provided errors.
    /// </summary>
    /// <param name="errors"></param>
    public InvalidException(List<string> errors)
        : base(ErrorCodes.ValidationExceptionErrorCode, ErrorCodes.ValidationExceptionDefaultMsg)
    {
        this.Errors = errors;
        this.Description = errors.FirstOrDefault();
        this.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
    }
    
    /// <summary>
    /// Create validation exception with provided IdentityErrors.
    /// </summary>
    /// <param name="errors"></param>
    public InvalidException(IEnumerable<IdentityError> errors)
        : base(ErrorCodes.ValidationExceptionErrorCode, ErrorCodes.ValidationExceptionDefaultMsg)
    {
        var identityErrors = errors.ToList();
        this.Errors = identityErrors.Select(x=> x.Description).ToList();
        this.Description = identityErrors.FirstOrDefault()?.Description;
        this.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
    }
    
    /// <summary>
    /// Create validation exception with provided IdentityErrors.
    /// </summary>
    /// <param name="errors"></param>
    public InvalidException(IEnumerable<ValidationFailure> errors)
        : base(ErrorCodes.ValidationExceptionErrorCode, ErrorCodes.ValidationExceptionDefaultMsg)
    {
        var validaitonErrors = errors.ToList();
        this.Errors = validaitonErrors.Select(x=> x.ErrorMessage).ToList();
        this.Description = validaitonErrors.FirstOrDefault()?.ErrorMessage;
        this.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
    }


    /// <summary>
    /// Create validation exception with default error message.
    /// </summary>
    public InvalidException()
        : base(ErrorCodes.ValidationExceptionErrorCode, ErrorCodes.ValidationExceptionDefaultMsg)
    {
        this.Description = ErrorCodes.ValidationExceptionDefaultMsg;
        this.HttpStatusCode = HttpStatusCode.UnprocessableEntity;
    }
}