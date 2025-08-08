using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Toodo.Application.Common.Exceptions;

namespace Toodo.API.Infrastructure;

public class ExceptionHandler() : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _handlers;

    public ExceptionHandler(ILogger<ExceptionHandler> logger) : this()
    {
        _logger = logger;

        _handlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();
        if (_handlers.TryGetValue(exceptionType, out var handler))
        {
            await handler(httpContext, exception);
        }
        else
        {
            await HandleInternalServerErrorException(httpContext, exception);
        }

        return true;
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(new HttpValidationProblemDetails(exception.Errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation Error",
            Detail = exception.Message
        });
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        });
    }

    private async Task HandleInternalServerErrorException(HttpContext httpContext, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Internal Server Error",
            Detail = "An internal server error occurred. See logs for more details."
        });
    }
}