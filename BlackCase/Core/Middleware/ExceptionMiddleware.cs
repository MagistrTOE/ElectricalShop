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
                await SendHttpResponse(ex, context, StatusCodes.Status404NotFound, ex.Message);
            }

            catch (DatabaseException ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status400BadRequest, ex.Message);
            }
           
            catch (Exception ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task SendHttpResponse( Exception ex, HttpContext context, int statusCode, string message)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync(message);
        }
    }
}
