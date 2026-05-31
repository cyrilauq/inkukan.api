using Inkukan.Domain.Entities;
using Inkukan.Domain.Repositories;
using Inkukan.Infrastructure.Data;
using Inkukan.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inkukan.Infrastructure
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
                .AddScoped<ITypeRepository, TypeRepository>()
                .AddScoped<ICollectionRepository, CollectionRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IBaseRepository<MangaSerie>, MangaSerieRepository>()
                .AddScoped<IBaseRepository<MangaPeople>, MangaPeopleRepository>()
                .AddScoped<IBaseRepository<Editor>, EditorRepository>()
                .AddScoped<IBaseRepository<SerieVolume>, SerieVolumeRepository>()
                .AddScoped<IBaseRepository<MangaType>, TypeRepository>()
                .AddScoped<IBaseRepository<MangaCollection>, CollectionRepository>()
                .AddScoped<IBlobStorage, VercelBlobStorage>();

            return services;
        }

        private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContextFactory<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString),
                ServiceLifetime.Scoped); // <--- THIS IS CRITICAL

            // 2. This satisfies Identity's need for a standard DbContext in the DI container
            services.AddScoped(p =>
                p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext());

            // 3. Fix the 'IDataProtectionProvider' error
            services.AddDataProtection();

            // 4. Identity Configuration
            services.AddIdentityCore<User>(cfg =>
            {
                cfg.SignIn.RequireConfirmedEmail = false;
                cfg.Password.RequiredLength = 10;
            })
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static async Task<IServiceProvider> ExecuteMigrationsAsync(this IServiceProvider services)
        {
            ApplicationDbContext dbContext = services.GetRequiredService<ApplicationDbContext>();
            RoleManager<Role> roleManager = services.GetRequiredService<RoleManager<Role>>();
            UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
            await dbContext.Database.MigrateAsync();

            if(await dbContext.MangaTypes.CountAsync(mt => mt.Code == "seinen") == 0)
                dbContext.MangaTypes.Add(new() { Code = "seinen", Name = "Seinen" });
            if (await dbContext.MangaCollections.CountAsync(mt => mt.Code == "seinen") == 0)
                dbContext.MangaCollections.Add(new() { Code = "seinen", Name = "Seinen" });

            if (await roleManager.Roles.CountAsync() == 0)
            {
                await roleManager.CreateAsync(new Role { Name = "User" });
                await roleManager.CreateAsync(new Role { Name = "Admin" });

                await userManager.CreateAsync(
                    new User
                    {
                        Email = "cyrilauqier@hotmail.fr",
                        Firstname = "Cyril",
                        Lastname = "Auquier",
                        UserName = "admin"
                    }, 
                    "Password123$"
                );
            }

            await dbContext.SaveChangesAsync();

            return services;
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "InkShelf.Api");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
