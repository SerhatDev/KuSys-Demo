using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using KuSys.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KuSys.Core.AttributeFilters;

/// <summary>
/// Auto validate Request items.
/// </summary>
public class ValidateRequestAttribute : Attribute, IActionFilter
{
    /// <summary>
    /// Validation process.
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="InvalidException"></exception>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var argToValidate = context.ActionArguments.FirstOrDefault(x => x.Value.GetType().GetInterface("IBaseRequest")!=null).Value;
        
        if(argToValidate is null) // No arguments has been marked as 'IBaseRequest' so we dont need to validate anything and skip.
            return;
        
        // Get the type of request
        var argType = argToValidate.GetType();
        
        // Get the type of the validator for this specific request model
        var objectType = typeof(IValidator<>).MakeGenericType(argType);
        
        // Get required validator service for request type if theres one
        var validatorBase = context.HttpContext.RequestServices.GetService(objectType);
        
        if(validatorBase is null) // Validator class wasn't created for this type, so we skip.
            return;
      
        // Get the 'Validate' method with reflection.
        MethodInfo methodInfo = validatorBase.GetType().GetMethods()
            .FirstOrDefault(x => x.ToString().Contains(argType.ToString()));
        
        // Execute 'validate' method with the request
        ValidationResult result =(ValidationResult) methodInfo.Invoke(validatorBase, new object[] { argToValidate });

        // If validation wasn't successful, throw valdation error for GlobalErrorHandler.
        if (!result.IsValid)
            throw new InvalidException(result.Errors);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No impl. needed
    }
}