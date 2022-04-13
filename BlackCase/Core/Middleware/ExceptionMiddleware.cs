using Microsoft.AspNetCore.Http;
using Core.Exceptions;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
            
            catch (IdentityException ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status400BadRequest, new { Errors = ex.Message });
            }

            catch (ArgumentNotFoundException ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status404NotFound, new { Errors = ex.Message });
            }

            catch (DatabaseException ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status400BadRequest, new { Errors = ex.Message });
            }
           
            catch (Exception ex)
            {
                await SendHttpResponse(ex, context, StatusCodes.Status500InternalServerError, new { Errors = ex.Message });
            }
        }

        private async Task SendHttpResponse( Exception ex, HttpContext context, int statusCode, object errors)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
        }
    }
}
