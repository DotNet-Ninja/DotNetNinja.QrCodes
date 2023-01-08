using DotNetNinja.AutoBoundConfiguration;
using DotNetNinja.QrCodes.Configuration;

namespace DotNetNinja.QrCodes;

public class StartUp
{
    public IConfiguration Configuration { get; }

    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var settingsProvider = services.AddAutoBoundConfigurations(Configuration).FromAssembly(typeof(Program).Assembly).Provider;

        services
            //.AddDataContext<DataContext>(settingsProvider)
            .AddMemoryCache()
            .AddApplicationServices()
            .AddAuthentication(settingsProvider)
            .AddPolicyBasedAuthorization()
            .AddControllersWithViews().Services
            .AddApplicationHealthChecks(settingsProvider);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ExceptionSettings exceptionSettings)
    {
        app.UseExceptionHandling(env, exceptionSettings)
            .UseHsts(env)
            .UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseApplicationEndpoints();

        //migrator.Migrate().SeedData();
    }
}