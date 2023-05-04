using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Repository;
using WebApi.Services.RegionServices;
using WebApi.Services.WalkServices;

namespace WebApi.ConfigureServices;

static public class ConfigureServiceExtension
{
    public static IServiceCollection ConfigureServicesRepAndServices(this IServiceCollection services)
    {
        services.AddScoped<IRegionRepository, SQLRegionRepository>();
        services.AddScoped<IWalkRepository, SQLWalkRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IImageRepository, LocalImageRepository>();

        services.AddScoped<IWalksService, WalksServices>();
        services.AddScoped<IRegionServices, RegionServices>();

        return services;
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(op =>
        {
            op.Password.RequireDigit = false;
            op.Password.RequireLowercase = false;
            op.Password.RequireNonAlphanumeric = false;
            op.Password.RequireUppercase = false;
            op.Password.RequiredLength = 6;
            op.Password.RequiredUniqueChars = 1;
        });
    }

    public static void SwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Walks Api", Version = "v1 " });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = "Oauth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

}
