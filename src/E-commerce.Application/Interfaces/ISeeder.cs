namespace E_commerce.Application.Interfaces;

public interface ISeeder<T> where T : class
{
    Task SeedAsync();
}

public interface ISeeder
{
    Task SeedAsync();
}
