`Swisschain.Extensions.EfCore`

Entity Framework Core extensions

# Usage

To run DB schema migration on the service startup add next code to you DI container configuration:

```c#
services.AddEfCoreDbMigration(c =>
{
    c.UseDbContextFactory(s => s.GetRequiredService<MyDatabaseContext>());
});
```

It's reasonable to do this in the worker (background) unit. If your service has several background units, do it only in the single unit.

To run DB schema validtion on the service startup add next code to you DI container configuration:

```c#
services.AddEfCoreDbValidation(c =>
{
    c.UseDbContextFactory(s => s.GetRequiredService<MyDatabaseContext>());
});
```

It's reasonable to do this in API units and secondary worker units.