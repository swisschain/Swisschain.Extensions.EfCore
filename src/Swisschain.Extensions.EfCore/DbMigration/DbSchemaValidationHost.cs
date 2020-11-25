using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Swisschain.Extensions.EfCore.DbMigration
{
    internal class DbSchemaValidationHost : IHostedService
    {
        private readonly ILogger<DbSchemaValidationHost> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly EfCoreDbValidationOptions _options;

        public DbSchemaValidationHost(ILogger<DbSchemaValidationHost> logger,
            IServiceProvider serviceProvider,
            EfCoreDbValidationOptions options)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("EF DB schema validation is being started...");

            await using var context = _options.DbContextFactory.Invoke(_serviceProvider);
            
            for (var take = 0; ;++take)
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);

                if (pendingMigrations.Any())
                {
                    if (take == 9)
                    {
                        throw new InvalidOperationException("There are pending migrations, service will be terminated");
                    }
                    _logger.LogError("There are pending migrations, will be retried in 10 seconds");
                }
                else
                {
                    break;
                }
            }

            _logger.LogInformation("EF DB schema validation has been completed.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
