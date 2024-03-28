using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CriptografiaAPI.Application.Core
{
    public static class DependencyInjection
    {
        public static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }
    }
}
