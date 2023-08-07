using FluentValidation;
using KuSys.Contracts.RequestModels;
using KuSys.Core.Helpers;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Request validation for new student.
/// </summary>
public sealed class NewStudentRequestValidation
    : AbstractValidator<NewStudentRequestModel>
{
    /// <summary>
    /// Validation rules.
    /// </summary>
    public NewStudentRequestValidation()
    {
        RuleFor(x => x.Email)
            .Must(x => x.IsCorrectMailFormat()).WithMessage("Email format wasn't in correct format");
        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender value was not correct!");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name can not be empty!")
            .NotNull().WithMessage("First name can not be empty!");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name can not be empty!")
            .NotNull().WithMessage("Last name can not be empty!");
        RuleFor(x=> x.Password)
            .NotEmpty().WithMessage("Password can not be empty!")
            .NotNull().WithMessage("Password can not be empty!");
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName can not be empty!")
            .NotNull().WithMessage("UserName can not be empty!")
            .Must(x => x.IsValidUserName()).WithMessage("User name can not contain special chars!");
    }
}