using InkShelf.Infrastructure;

namespace InkShelf.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructure(configuration);

            return services;
        }
    }
}
