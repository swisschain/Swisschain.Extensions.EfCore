using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Swisschain.Extensions.EfCore.DbMigration
{
    internal class DbSchemaMigrationHost : IHostedService
    {
        private readonly ILogger<DbSchemaMigrationHost> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly EfCoreDbMigrationOptions _options;

        public DbSchemaMigrationHost(ILogger<DbSchemaMigrationHost> logger, 
            IServiceProvider serviceProvider,
            EfCoreDbMigrationOptions options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("EF DB schema migration is being started...");

            await using var context = _options.DbContextFactory.Invoke(_serviceProvider);

            await context.Database.MigrateAsync(cancellationToken);

            _logger.LogInformation("EF DB schema migration has been completed.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
