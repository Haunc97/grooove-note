using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace PersonalNotesAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizationTokenCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationTokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationTokenCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationTokenCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationTokenCheckMiddleware>();
        }
    }
}
