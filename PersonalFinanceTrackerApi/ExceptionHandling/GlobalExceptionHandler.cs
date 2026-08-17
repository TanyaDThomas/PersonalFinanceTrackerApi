using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Application.Exceptions;
using System.Reflection;

namespace PersonalFinanceTracker.Api.ExceptionHandling
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if(exception is NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                await httpContext.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Resource not found",
                        Detail = notFoundException.Message,
                        Instance = httpContext.Request.Path
                    },
                    cancellationToken);
                return true;
            }

            if(exception is ConflictException conflictException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

                await httpContext.Response.WriteAsJsonAsync(
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = $"Conflict {conflictException.Message}",
                        Detail = conflictException.Message,
                        Instance = httpContext.Request.Path
                    }, 
                    cancellationToken);
                return true;

            }

            _logger.LogError("UnhandledExceptionOccured");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred.Please contact support if the problem persists",
                    Instance = httpContext.Request.Path
                },
                cancellationToken);
            return true;
        }
    }
}
