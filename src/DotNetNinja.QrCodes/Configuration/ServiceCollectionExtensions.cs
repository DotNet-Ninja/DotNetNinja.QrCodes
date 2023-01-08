using Auth0.AspNetCore.Authentication;
using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.QrCodes.Constants;
using DotNetNinja.QrCodes.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetNinja.QrCodes.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddScoped<ITimeProvider, SystemTimeProvider>()
            .AddScoped<ISignInService, SignInService>();
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
    {
        var settings = provider.Get<AuthenticationSettings>();
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = settings.Domain;
            options.ClientId = settings.ClientId;
            options.Scope = string.Join(' ', "email", options.Scope);
        });
        return services;
    }

    public static IServiceCollection AddPolicyBasedAuthorization(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            options.AddPolicy(Roles.Admin, policy =>
            {
                policy.RequireRole(Roles.Admin);
            });
        });
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
        where TContext : DbContext
    {
        string connectionName = typeof(TContext).Name;
        return services.AddDataContext<TContext>(provider, connectionName);
    }

    public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services,
        IAutoBoundConfigurationProvider provider, string connectionName) where TContext : DbContext
    {
        //var settings = provider.Get<DbSettings>();
        //services.AddScoped<IDbMigrator<DataContext>, SqlDbMigrator<DataContext>>();
        //return services
        //    .AddDbContext<TContext>(options =>
        //        options.UseSqlServer(settings.Contexts[connectionName].ConnectionString));
        return services;
    }

    public static IServiceCollection AddApplicationHealthChecks(this IServiceCollection services, IAutoBoundConfigurationProvider provider)
    {
        var dbSettings = provider.Get<DbSettings>();
        var checks = services.AddHealthChecks();
        foreach (var db in dbSettings.Contexts)
        {
            checks = checks.AddSqlServer(db.Value.ConnectionString, name: db.Value.Name, tags: new[]
            {
                "Database",
                "SqlServer"
            });
        }

        return checks.Services;
    }
}