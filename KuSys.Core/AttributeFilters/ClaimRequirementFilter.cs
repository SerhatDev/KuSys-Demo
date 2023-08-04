using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KuSys.Core.AttributeFilters;

/// <summary>
/// Attribute for specifying Claim requirements while Authorization.
/// </summary>
public class ClaimRequirementAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="claimType">Type of the claim (US-1,US-2,US-3,US-4)</param>
    public ClaimRequirementAttribute(string claimType) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, string.Empty) };
    }
}

/// <summary>
/// Authorization filter for defining claim reuqirements.
/// </summary>
public class ClaimRequirementFilter : IAuthorizationFilter
{
    readonly Claim _claim;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="claim"></param>
    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }

    /// <summary>
    /// Process to check if user has required claims for the operation.
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {   
        var hasClaim = context.HttpContext.User.HasClaim(c => c.Type == _claim.Type);
        if (!hasClaim)
        {
            context.Result = new ForbidResult();
        }
    }
}