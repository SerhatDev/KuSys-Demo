using FluentValidation;
using KuSys.Contracts.RequestModels;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Validation class for adding new course.
/// </summary>
public sealed class NewCourseRequestValidation
    : AbstractValidator<NewCourseRequestModel>
{
    /// <summary>
    /// Validation rules.
    /// </summary>
    public NewCourseRequestValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Course code can not me empty!")
            .Length(3,12).WithMessage("Course code should be minimum 3 maximum 12 chars.")
            .NotNull().WithMessage("Course code can not me empty!");
        RuleFor(x=> x.Name)
            .NotEmpty().WithMessage("Course name can not me empty!")
            .Length(3,50).WithMessage("Course name should be minimum 3 maximum 50 chars.")
            .NotNull().WithMessage("Course name can not me empty!");
    }
}