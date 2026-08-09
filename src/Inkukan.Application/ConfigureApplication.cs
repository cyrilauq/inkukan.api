using FluentValidation;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Mediator.Extensions;
using Inkukan.Application.Services;
using Inkukan.Application.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Inkukan.Application;

public static class ConfigureApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMapper(configuration)
            .AddMediator(typeof(ConfigureApplication).Assembly)
            .AddServices(configuration);

        services.AddValidatorsFromAssemblyContaining<CreateMangaSerieValidator>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<TokenConfiguration>(configuration.GetSection(nameof(TokenConfiguration)));

        services
            .AddScoped<IFileUploader, FileUploaderVercelBlob>()
            .AddScoped<IFileChecker, FileChecker>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IHashService, SHAHashService>()
            .AddScoped<ITraceIdAccessor, TraceIdAccessor>();

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.LicenseKey = configuration.GetSection("LicenseKeys").GetSection("LuckyPennySoftware").Value ?? throw new ArgumentException("The [LuckyPennySoftware]'s key should be specified");
        }, typeof(ConfigureApplication).Assembly);

        return services;
    }
}
