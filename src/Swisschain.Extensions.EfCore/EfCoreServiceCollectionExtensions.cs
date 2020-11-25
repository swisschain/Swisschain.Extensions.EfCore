using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swisschain.Extensions.EfCore.DbMigration;

namespace Swisschain.Extensions.EfCore
{
    public static class EfCoreServiceCollectionExtensions
    {
        public static IServiceCollection AddEfCoreDbMigration(this IServiceCollection services, Action<EfCoreDbMigrationOptions> config)
        {
            var options = new EfCoreDbMigrationOptions();

            config.Invoke(options);

            services.AddHostedService(c => new DbSchemaMigrationHost(
                c.GetRequiredService<ILogger<DbSchemaMigrationHost>>(),
                c,
                options));

            return services;
        }

        public static IServiceCollection AddEfCoreDbValidation(this IServiceCollection services, Action<EfCoreDbValidationOptions> config)
        {
            var options = new EfCoreDbValidationOptions();

            config.Invoke(options);

            services.AddHostedService(c => new DbSchemaValidationHost(
                c.GetRequiredService<ILogger<DbSchemaValidationHost>>(),
                c,
                options));

            return services;
        }
    }
}
