using Inkukan.Api.Middlewares;
using Inkukan.Application;
using Inkukan.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text;

namespace Inkukan.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddTraceAndTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName: "Inkukan.Api"))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .SetErrorStatusOnException());

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplicationCors(configuration)
            .AddInfrastructure(configuration)
            .AddApplication(configuration)
            .AddLogging()
            .AddTransient<ExceptionMiddleware>()
            .AddJwtAuthorization(configuration);

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("TokenConfiguration").GetSection("SecretKey").Value ?? throw new ArgumentNullException("No key was provided for jwt authorization"))),
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        services.AddAuthorization();
        return services;
    }

    private static IServiceCollection AddApplicationCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CORS", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return services;
    }

    public static IApplicationBuilder UseApplicationCors(this IApplicationBuilder builder, IConfiguration configuration)
    {
        builder.UseCors("CORS");

        return builder;
    }
}
