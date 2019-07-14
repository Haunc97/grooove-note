using Microsoft.AspNetCore.Builder;
using PersonalNotesAPI.Middlewares;

namespace PersonalNotesAPI
{
    public partial class Startup
    {
        public void RegisterMiddlewares(IApplicationBuilder builder)
        {
            builder.UseMiddleware<CheckBrowserMiddleware>();
        }
    }
}
