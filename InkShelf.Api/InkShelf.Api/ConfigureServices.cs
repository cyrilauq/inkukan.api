using InkShelf.Api.Middlewares;
using InkShelf.Application;
using InkShelf.Application.Services.Implementations;
using InkShelf.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InkShelf.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplicationCors(configuration)
                .AddInfrastructure(configuration)
                .AddApplication(configuration)
                .AddLogging()
                .AddTransient<ExceptionMiddleware>()
                .AddJwtAuthorization(configuration);

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
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[$"TokenConfiguration:SecretKey"] ?? throw new ArgumentNullException("No key was provided for jwt authorization"))),
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
}
