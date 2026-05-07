using FluentValidation;
using InkShelf.Application.Features.MangaSerie.Command.Create;
using InkShelf.Application.Services;
using InkShelf.Application.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace InkShelf.Application
{
    public static class ConfigureApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMapper(configuration)
                .AddMediator(configuration)
                .AddServices(configuration);

            services.AddValidatorsFromAssemblyContaining<CreateMangaSerieValidator>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            VercelBlobOptions blobOptions = new();
            configuration.GetSection(nameof(VercelBlobOptions)).Bind(blobOptions);
            services
                .Configure<TokenConfiguration>(configuration.GetSection(nameof(TokenConfiguration)))
                .AddSingleton(blobOptions);

            services.AddHttpClient("VercelBlocClient", client =>
            {
                client.BaseAddress = new(blobOptions.BlobUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", blobOptions.Token);
                client.DefaultRequestHeaders.Add("x-add-random-suffix", "true");
            });

            services
                .AddScoped<IFileUploader, FileUploaderVercelBlob>()
                .AddScoped<IFileChecker, FileChecker>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IHashService, SHAHashService>();

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = configuration["LicenseKeys:LuckyPennySoftware"] ?? throw new ArgumentException("The [LuckyPennySoftware]'s key should be specified");
            }, typeof(ConfigureApplication).Assembly);

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
