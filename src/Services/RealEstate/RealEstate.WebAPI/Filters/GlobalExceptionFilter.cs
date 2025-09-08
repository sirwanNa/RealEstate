using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RealEstate.WebAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var clientIP = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            _logger.LogError(exception, "Unhandled exception occurred. ClientIP: {ClientIP}", clientIP);

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred.",
                ClientIP = clientIP
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
