using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace PersonalNotesAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckBrowserMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckBrowserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Headers["User-Agent"].Any(h => h.ToLower().Contains("chrome")))
            {
                await _next.Invoke(httpContext);
            }
            else
            {
                HttpResponse response = httpContext.Response;
                response.StatusCode = StatusCodes.Status400BadRequest;
                await response.WriteAsync("Please using chrome to request.", Encoding.UTF8);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckBrowserMWExtensions
    {
        public static IApplicationBuilder UseCheckBrowserMW(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckBrowserMiddleware>();
        }
    }
}
