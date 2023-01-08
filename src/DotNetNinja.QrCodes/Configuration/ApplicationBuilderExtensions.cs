namespace DotNetNinja.QrCodes.Configuration;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationEndpoints(this IApplicationBuilder app)
    {
        return app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("healthz");
            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //endpoints.MapControllerRoute(
            //    name: "RedirectById",
            //    pattern: "/{**id}",
            //    defaults: new { controller = "Redirect", action = "Redirect" });
        });
    }

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment, ExceptionSettings settings)
    {
        if (environment.IsDevelopment() && settings.EnableDeveloperExceptionsPage)
        {
            return app.UseDeveloperExceptionPage();
        }

        return app.UseExceptionHandler("/Error");
    }

    public static IApplicationBuilder UseHsts(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            app.UseHsts();
        }

        return app;
    }
}