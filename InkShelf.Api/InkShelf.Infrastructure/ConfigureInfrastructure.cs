using InkShelf.Domain.Repositories;
using InkShelf.Infrastructure.Data;
using InkShelf.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InkShelf.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFramework(configuration)
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IMangaSerieRepository, MangaSerieRepository>()
                .AddScoped<IMangaPeopleRepository, MangaPeopleRepository>()
                .AddScoped<IEditorRepository, EditorRepository>()
                .AddScoped<ISerieVolumeRepository, SerieVolumeRepository>()
                .AddScoped<IBlobStorage, VercelBlobStorage>();

            return services;
        }

        private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static async Task<IServiceProvider> ExecuteMigrationsAsync(this IServiceProvider services)
        {
            ApplicationDbContext dbContext = services.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();

            if(await dbContext.MangaTypes.CountAsync(mt => mt.Code == "seinen") == 0)
                dbContext.MangaTypes.Add(new() { Code = "seinen", Name = "Seinen" });
            if (await dbContext.MangaCollections.CountAsync(mt => mt.Code == "seinen") == 0)
                dbContext.MangaCollections.Add(new() { Code = "seinen", Name = "Seinen" });

            await dbContext.SaveChangesAsync();

            return services;
        }
    }
}
