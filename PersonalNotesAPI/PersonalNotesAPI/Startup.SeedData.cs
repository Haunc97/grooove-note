using Microsoft.AspNetCore.Builder;
using System;

namespace PersonalNotesAPI
{
    public partial class Startup
    {
        public void RegisterSeedData(IApplicationBuilder builder, IServiceProvider services)
        {
           //SeedDatabase.Initialize(builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

           //SeedDatabase.CreateUserRoleAsync(services).Wait();
        }
    }
}
