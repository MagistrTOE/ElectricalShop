using Microsoft.AspNetCore.Http;
using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }

            catch (ArgumentNotFoundException ex)
            {
                await SendHttpResponse(_logger, ex, context, StatusCodes.Status404NotFound, ex.Message);
            }
           
            catch (Exception ex)
            {
                await SendHttpResponse(_logger, ex, context, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private static async Task SendHttpResponse(ILogger<ExceptionMiddleware> logger, Exception ex, HttpContext context, int statusCode, string message)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync(message);
        }
    }
}
