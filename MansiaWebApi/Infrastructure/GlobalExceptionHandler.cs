using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace MansiaWebApi.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        } 
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception, 
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Exception Occurs: {exception.Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Title = "Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Type = "",
                Detail = exception.InnerException?.Message.ToString(),
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
