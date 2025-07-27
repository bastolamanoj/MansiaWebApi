using DataProvider.Interfaces;
using DataProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace MansiaWebApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IExceptionLoggerRepository exceptionLogger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger,IExceptionLoggerRepository exceptionLoggerRepository)
        {
            _next= next;    
            _logger= logger;
            exceptionLogger = exceptionLoggerRepository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Exception Occurs: {exception.Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = "Server Error",
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "",
                    Detail = exception.InnerException?.Message.ToString(),
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(problemDetails);
                if (true)
                {
                    ExceptionLogger ex = new ExceptionLogger() { 
                        Id = Guid.NewGuid(),
                        ExceptionMessage = exception.Message,
                        ControllerName = exception.GetType().Name,  
                        ExceptionStackTrace = exception.Message.ToString(),
                        LogTime = DateTime.Now,
                        UserId = Guid.NewGuid()
                    };
                    exceptionLogger.Add(ex);
                }
            }
        }
    }
}
