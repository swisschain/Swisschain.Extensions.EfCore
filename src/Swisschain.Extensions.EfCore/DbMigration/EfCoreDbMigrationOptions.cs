using System;
using Microsoft.EntityFrameworkCore;

namespace Swisschain.Extensions.EfCore.DbMigration
{
    public sealed class EfCoreDbMigrationOptions
    {
        internal EfCoreDbMigrationOptions()
        {
            
        }

        public Func<IServiceProvider, DbContext> DbContextFactory { get; private set; }

        public EfCoreDbMigrationOptions UseDbContextFactory(Func<IServiceProvider, DbContext> factory)
        {
            DbContextFactory = factory;

            return this;
        }
    }
}
