using Demo.Core.Application;
using Demo.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Demo;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages(options => {
            options.Conventions.AllowAnonymousToPage("/Index");
            options.Conventions.AllowAnonymousToPage("/Auth/Login");
            options.Conventions.AllowAnonymousToPage("/Auth/Logout");
            options.Conventions.AllowAnonymousToPage("/Auth/Register");
            options.Conventions.AllowAnonymousToPage("/Auth/Forbidden");
        }).AddRazorRuntimeCompilation();
        
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/Forbidden";
            });
        services.AddAuthorization();
        services.AddApplication();
        services.AddLogging();
        services.AddInfrastructure(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => {
            endpoints.MapRazorPages().RequireAuthorization();
        });
        app.ApplicationServices.ApplyDatabaseMigrations();
    }
}