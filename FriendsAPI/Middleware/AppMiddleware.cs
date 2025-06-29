using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainAPI.Middleware
{
    public class AppMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AppMiddleware> _logger;

        public AppMiddleware(RequestDelegate next, ILogger<AppMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // ✅ Extract token
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (!string.IsNullOrEmpty(token))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jwtToken != null && jwtToken.ValidTo > System.DateTime.UtcNow)
                    {
                        var claims = jwtToken.Claims;
                        context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                    }
                    else
                    {
                        _logger.LogWarning("Expired or invalid JWT token.");
                    }
                }

                // ✅ Proceed to next middleware
                await _next(context);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in middleware.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = context.Response.StatusCode,
                message = "An unexpected error occurred.",
                detail = exception.Message // remove in prod for security
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
