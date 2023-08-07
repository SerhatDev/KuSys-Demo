using FluentValidation;
using KuSys.Contracts.RequestModels;
using KuSys.Core.Helpers;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Register Request validation.
/// </summary>
public class RegisterRequestValidation 
    : AbstractValidator<RegisterRequestModel>
{
    /// <summary>
    /// Register validation rules.
    /// </summary>
    public RegisterRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can not be empty")
            .NotNull().WithMessage("Email can not be empty")
            .Must(x=> x.IsCorrectMailFormat()).WithMessage("Email was not in correct format!");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name can not be empty")
            .NotNull().WithMessage("First name can not be empty");
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Email can not be empty")
            .NotNull().WithMessage("Email can not be empty")
            .Must(x=> x.IsValidUserName()).WithMessage("User name can not have special charachters!")
            .MinimumLength(5).WithMessage("User name can not be shorter than 5 chars");
    }
}