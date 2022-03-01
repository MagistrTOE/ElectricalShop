using Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Extension
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
