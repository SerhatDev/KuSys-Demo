using FluentValidation;
using KuSys.Contracts.RequestModels;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Validation for selecting course for student.
/// </summary>
public sealed class SelectCourseValidation
    : AbstractValidator<SelectCourseRequest>
{
    /// <summary>
    /// Validation rules.
    /// </summary>
    public SelectCourseValidation()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId can not be empty")
            .NotNull().WithMessage("CourseId can not be empty");
    }
}