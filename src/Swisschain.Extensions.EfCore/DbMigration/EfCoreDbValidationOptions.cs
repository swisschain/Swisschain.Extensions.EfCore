using System;
using Microsoft.EntityFrameworkCore;

namespace Swisschain.Extensions.EfCore.DbMigration
{
    public sealed class EfCoreDbValidationOptions
    {
        internal EfCoreDbValidationOptions()
        {

        }

        public Func<IServiceProvider, DbContext> DbContextFactory { get; private set; }

        public EfCoreDbValidationOptions UseDbContextFactory(Func<IServiceProvider, DbContext> factory)
        {
            DbContextFactory = factory;

            return this;
        }
    }
}
