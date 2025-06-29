using Microsoft.AspNetCore.Builder;

namespace MainAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAppMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MainAPI.Middleware.AppMiddleware>();
        }
    }
}
