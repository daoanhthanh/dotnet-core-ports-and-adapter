using System.Text.Json.Serialization;
using Adapter.Out.Postgres.Config;
using Domain.Services.AutoMapper;
using Microsoft.AspNetCore.Mvc.Versioning;
using Src.Extensions;
using Src.IoC;

namespace Src;

public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();
        
        services.AddDistributedMemoryCache();

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        
        // services.AddHttpContextAccessor();

        services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "JVI";
        });

        services.AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        // ----- AutoMapper -----
        services.AddAutoMapper(typeof(AutoMapperConfig));

        // ----- Add Databases -----
        services.AddPostgresDatabase(configuration, env.IsProduction());
        // services.AddGarnetCacheDatabase(configuration);

        // ----- Register dependencies for services -----
        DependenciesInjector.RegisterServices(services);

        if (env.IsDevelopment() || env.IsEnvironment("Local"))
        {
            services.AddCustomizedSwagger();
        }

        // ----- Versioning -----
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        // ----- Add ApiExplorer to discover versions -----
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        
        // ----- Migrate database -----
        // services.MigrateDatabase(configuration.GetConnectionString("Postgres:RootConnection"));
        
    }

    public void Configure(IApplicationBuilder app)
    {
        if (env.IsEnvironment("Local") || env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseCustomizedSwagger();
        }
        
        app.UseRouting();

        app.UseAuthorization();
        
        app.UseAuthentication();
        
        app.UseSession();  

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}