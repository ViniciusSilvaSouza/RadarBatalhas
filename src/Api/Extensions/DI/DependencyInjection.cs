using Api.TestSupport;
using Application.Profiles;
using Domain.Compartilhado.Contracts;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Radar.Infrastructure.Cache;
using StackExchange.Redis;
using Api.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddRadarDependencyInjection(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddControllers();
        services.AddAutoMapper(typeof(DomainToDtoProfile).Assembly);
        services.AddHealthChecks();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        bool isTest = string.Equals(env.EnvironmentName, "Test", StringComparison.OrdinalIgnoreCase);
        bool useInMemoryDb = string.Equals(Environment.GetEnvironmentVariable("DB_HOST"), "INMEMORY", StringComparison.OrdinalIgnoreCase);
        bool runningInContainer = string.Equals(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), "true", StringComparison.OrdinalIgnoreCase);

        static string EnvOrDefault(string key, string @default)
        {
            var v = Environment.GetEnvironmentVariable(key);
            return string.IsNullOrWhiteSpace(v) ? @default : v!;
        }

        var dbHost = EnvOrDefault("DB_HOST", runningInContainer ? "radar" : "localhost");
        var dbPort = EnvOrDefault("DB_PORT", runningInContainer ? "3307" : "3307");
        var dbName = EnvOrDefault("DB_NAME", "radar");
        var dbUser = EnvOrDefault("DB_USER", "radar");
        var dbPass = EnvOrDefault("DB_PASSWORD", "radar123");
        var redisHost = EnvOrDefault("REDIS_HOST", runningInContainer ? "redis:6379" : "localhost:6379");
        var kcAuthUrlBase = EnvOrDefault("KEYCLOAK_AUTH_URL", runningInContainer ? "http://keycloak:8080" : "http://localhost:8080");
        var kcRealm = EnvOrDefault("KEYCLOAK_REALM", "radar");
        var kcClientId = EnvOrDefault("KEYCLOAK_CLIENT_ID", "radar-api");

        // Database
        if (isTest || useInMemoryDb)
        {
            services.AddDbContext<RadarDbContext>(o => o.UseInMemoryDatabase("radar-tests"));
        }
        else
        {
            var cs = configuration.GetConnectionString("MySql")
                     ?? $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPass};";
            services.AddDbContext<RadarDbContext>(o => o.UseMySql(cs, ServerVersion.AutoDetect(cs)));
        }

        // Redis cache
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisHost));
        services.AddSingleton<ICacheService, RedisCacheService>();

        // Auth
        if (isTest)
        {
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("Test", _ => { });
        }
        else
        {
            var authority = kcAuthUrlBase.TrimEnd('/') + "/realms/" + kcRealm;
            var audience = kcClientId;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.Authority = authority;
                    o.Audience = audience;
                    o.RequireHttpsMetadata = false;
                    o.MapInboundClaims = false; // mantém claims como no JWT (sub, roles)
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }

        services.AddAuthorization(options =>
        {
            options.AddPolicy("eventos.criar", p => p.Requirements.Add(new PermissionRequirement("eventos.criar")));
            options.AddPolicy("admin.somente", p => p.Requirements.Add(new PermissionRequirement("noticias.publicar")));
        });
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();

        // UoW + App dependencies (repositories, use cases)
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRadarAppDependencies();

        return services;
    }
}
