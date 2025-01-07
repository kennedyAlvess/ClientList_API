using ClientListApi.Application.Services;
using ClientListApi.Domain.Repositories;
using ClientListApi.Infrastructure.Repository;
using ClientListApi.Security;
using Microsoft.EntityFrameworkCore;

namespace ClientListApi.Infrastructure.Config
{
    public static class InfraModules
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddDBContext()
                    .AddDependecyInjection();

            return services;
        }
        public static IServiceCollection AddDBContext(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            IConfiguration? config = serviceProvider.GetService<IConfiguration>();

            if (config?.GetConnectionString("DefaultConnection") != null)
            {
                services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly("Infrastructure"));
                    });
            }

            return services;
        }
        public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
        {
            services.AddScoped<IClienteServices, ClienteServices>();
            //services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<ITokenActions, TokenActions>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}
