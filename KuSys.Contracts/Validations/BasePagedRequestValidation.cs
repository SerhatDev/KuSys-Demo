using FluentValidation;
using KuSys.Contracts.RequestModels;

namespace KuSys.Contracts.Validations;

/// <summary>
/// Validaiton object for PagedRequests.
/// </summary>
public abstract class BasePagedRequestValidation<T>
    : AbstractValidator<T>
        where T: PagedRequest
{
    /// <summary>
    /// ValidationRules
    /// </summary>
    public BasePagedRequestValidation()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("Page number can not be 0 or negative values")
            .When(x => x.PageNumber is not null);
        RuleFor(x=> x.PageSize)
            .LessThanOrEqualTo(100).WithMessage("Page size can not be greater than 100")
            .GreaterThanOrEqualTo(1).WithMessage("Page size can not be 0 or negative values")
            .When(x => x.PageSize is not null);
    }
}