using FluentValidation;
using KuSys.Contracts.RequestModels;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Validation object for Update Student request.
/// </summary>
public sealed class UpdateStudentRequestValidation
    : AbstractValidator<UpdateStudentModel>
{
    /// <summary>
    /// Validation rules.
    /// </summary>
    public UpdateStudentRequestValidation()
    {
        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender value was not correct!");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name can not be empty!")
            .NotNull().WithMessage("First name can not be empty!");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name can not be empty!")
            .NotNull().WithMessage("Last name can not be empty!");
        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Birth date can not be empty!");
    }
}