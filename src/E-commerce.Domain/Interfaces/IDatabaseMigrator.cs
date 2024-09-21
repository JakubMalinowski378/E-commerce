namespace E_commerce.Domain.Interfaces;

public interface IDatabaseMigrator
{
    Task MigrateAsync();
}