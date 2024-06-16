using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapter.Out.Postgres.Config;

public static class PostgresDbConfig
{
    public static IServiceCollection AddPostgresDatabase(this IServiceCollection services,
        IConfiguration configuration, bool isProduction)
    {
        services.AddDbContext<PostgresRootDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Postgres:RootConnection"));

            if (isProduction) return;

            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        return services;
    }
}