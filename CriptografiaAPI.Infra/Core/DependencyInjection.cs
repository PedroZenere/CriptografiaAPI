using CriptografiaAPI.Infra.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CriptografiaAPI.Infra.Core
{
    public static class DependencyInjection
    {
        public static void ConfigureMainDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                SqlConnectionStringBuilder connectionStringBuilder = new(connectionString);

                options.UseSqlServer(
                    connectionStringBuilder.ConnectionString,
                    sqlOptions => sqlOptions.MigrationsHistoryTable("__MigrationHistory")
                );

                options
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .UseLoggerFactory(LoggerFactory.Create(builder => { builder.ToString(); }));
            });
        }
        public static void CheckConnectionDatabase(this IServiceCollection services)
        {
            //se o banco nao existir nao funciona
            var serviceProvider = services.BuildServiceProvider();
            using var db = serviceProvider.GetRequiredService<ApplicationContext>();
            if (!db.Database.EnsureCreated()) 
            {
                if (!db.Database.CanConnect())
                    throw new Exception("Connection unsuccessful");
                return;
            }
        }

        public static void RunMigrations(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using var db = serviceProvider.GetRequiredService<ApplicationContext>();
            db.Database.Migrate();
        }
    }
}
