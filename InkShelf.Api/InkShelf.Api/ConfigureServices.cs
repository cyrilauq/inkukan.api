using InkShelf.Api.Middlewares;
using InkShelf.Application;
using InkShelf.Infrastructure;

namespace InkShelf.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfrastructure(configuration)
                .AddApplication(configuration)
                .AddLogging()
                .AddTransient<ExceptionMiddleware>();

            return services;
        }
    }
}
