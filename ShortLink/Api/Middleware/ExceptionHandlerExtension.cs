using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using ShortLink.Services.Errors;

namespace ShortLink.Api.Middleware
{
    public static class ExceptionHandlerExtension
    {
        public static IApplicationBuilder UseExceptionHandlerExtension(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();
                var ex = exceptionHandlerPathFeature.Error;

                if (ex is ShortLinkNotFoundError)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }

                if (ex is GenerateShortLinkAttemptExceededError)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }));

            return app;
        }
    }
}