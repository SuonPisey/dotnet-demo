using System.Text.Json;
using MyApi.Models;

namespace MyApi.Middleware
{
    public class RequireHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public RequireHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;
            // allow login without header
            if (path == "/api/auth/login" && context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("Authorization") || string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"]))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var resp = new ApiResponse<object>(null, "Authorization header is required", false);
                await context.Response.WriteAsync(JsonSerializer.Serialize(resp));
                return;
            }

            await _next(context);
        }
    }
}
