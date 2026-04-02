using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InkShelf.Application
{
    public static class ConfigureApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMapper(configuration)
                .AddMediator(configuration);

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = configuration["LicenseKeys:LuckyPennySoftware"] ?? throw new ArgumentException("The [LuckyPennySoftware]'s key should be specified");

                cfg.AddMaps(typeof(ConfigureApplication).Assembly);
            });

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediatR(cfg => 
                {
                    cfg.LicenseKey = configuration["LicenseKeys:LuckyPennySoftware"] ?? throw new ArgumentException("The [LuckyPennySoftware]'s key should be specified"); ;
                    cfg.RegisterServicesFromAssembly(typeof(ConfigureApplication).Assembly);
                });

            return services;
        }
    }
}
