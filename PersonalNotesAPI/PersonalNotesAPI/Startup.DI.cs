using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using PersonalNotes.Service;
using PersonalNotesAPI.Auth;
using PersonalNotesAPI.Data;
using PersonalNotesAPI.Data.Infrastructure;
using PersonalNotesAPI.Data.Repositories;
using System.Linq;
using System.Reflection;

namespace PersonalNotesAPI
{
    public partial class Startup
    {
        public void RegisterDI(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserResolverService, UserResolverService>();
            services.AddSingleton<IAuthEmailSenderUtil,AuthEmailSenderUtil>();
            // Repos
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(NotebookRepos)))
                .Where(x => x.Name.EndsWith("Repos"))
                .AsPublicImplementedInterfaces();
            // Serivce
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(NotebookService)))
                .Where(x => x.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
        }
    }
}
