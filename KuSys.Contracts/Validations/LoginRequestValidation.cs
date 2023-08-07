using FluentValidation;
using KuSys.Contracts.RequestModels;
using KuSys.Core.Helpers;

namespace KuSys.Contracts.Validations;

/// <summary>
/// LoginRequest validaiton.
/// </summary>
public sealed class LoginRequestValidation
    : AbstractValidator<LoginRequestModel>
{
    /// <summary>
    /// Login request validation rules.
    /// </summary>
    public LoginRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can not be empty!")
            .NotNull().WithMessage("Email can not be null")
            .Must(x=> x.IsCorrectMailFormat()).WithMessage("Email format was not correct!");
    }

    
}