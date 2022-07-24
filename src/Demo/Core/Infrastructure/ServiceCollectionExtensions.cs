using System.Data;
using System.Reflection;
using Dapper;
using Demo.Core.Application.Contracts;
using Demo.Core.Infrastructure.Persistence;
using Demo.Core.Infrastructure.Repository;
using FluentMigrator.Runner;
using Npgsql;

namespace Demo.Core.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Db");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new Exception("Connection string missing");
        }

        services.AddScoped<ICurrentContext, CurrentContext>();
        services.AddHttpContextAccessor();
        
        services.AddScoped<IOrganisationRepository, OrganisationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DatabaseContext>(options => {
            options.UseNpgsql(connectionString);
            options.EnableDetailedErrors(true);
            options.EnableSensitiveDataLogging(true);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}