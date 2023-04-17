using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.ActiveDirectory.Services;
using Shared.Identity.Services;
using Shared.Utilities.Services;

namespace Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IActiveDirectoryService, ActiveDirectoryService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IArchivoService, ArchivoService>();
        }
    }
}
