using KuSys.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KuSys.Core.Middlewares;

/// <summary>
/// Middleware to handle exceptions globally.
/// </summary>
public sealed class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Global exception handler.
    /// </summary>
    /// <param name="next"></param>
    public GlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke to catch exceptions.
    /// </summary>
    /// <param name="httpContext"></param>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BaseException ex)
        {
            await HandleCustomException(ex, httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleCustomException(BaseException ex, HttpContext context)
    {
        var errorBuilder = new ExceptionBuilder();
        var error = errorBuilder
            .WithMessage(ex.Message)
            .WithErrorCode(ex.ErrorCode)
            .WithErrors(ex.Errors);
            
   
        var errorJson = error.ToJson();

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)ex.HttpStatusCode;
        await context.Response.WriteAsync(errorJson);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorBuilder = new ExceptionBuilder();
        var error = errorBuilder
            .WithMessage(exception.Message)
            .WithErrors(new List<string>()
            {
                exception.InnerException?.Message
            });

                
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync(error.ToJson());
    }
}