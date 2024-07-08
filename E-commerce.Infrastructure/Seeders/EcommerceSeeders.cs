using Bogus;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Constants;
using System.Data;
using System.Drawing;
using System.Text;

namespace E_commerce.Infrastructure.Seeders;
public class EcommerceSeeders(EcommerceDbContext dbContext) : IEcommerceSeeders
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.SaveChangesAsync();
        }
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Users.Any())
            {
                var users = GetUser();
                dbContext.Users.AddRange(users);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Addresses.Any())
            {
                var adresses = GetAddresses();
                dbContext.Addresses.AddRange(adresses);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Products.Any())
            {
                var products = GetProducts();
                dbContext.Products.AddRange(products);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Carts.Any())
            {
                var carts= GetCarts();
                dbContext.Carts.AddRange(carts);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.CartItems.Any())
            {
                var cartItems = GetCartItems();
                dbContext.CartItems.AddRange(cartItems);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Ratings.Any())
            {
                var ratings = GetRatings();
                dbContext.Ratings.AddRange(ratings);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.ProductCategories.Any())
            {
                var productCategories = GetProductCategories();
                dbContext.ProductCategories.AddRange(productCategories);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<User> GetUser()
    {
        var users = new Faker<User>()
                .RuleFor(x => x.Firstname, y => y.Name.FirstName())
                .RuleFor(x => x.LastName, y => y.Name.LastName())
                .RuleFor(x => x.Email, y => y.Internet.Email())
                .RuleFor(x => x.Gender, y => GenerateGender())
                .RuleFor(x => x.PhoneNumber, y => y.Person.Phone)
                .RuleFor(x => x.PasswordHash, y => GeneretePassword1())
                .RuleFor(x => x.PasswordSalt, y => GeneretePassword2())
                .RuleFor(x => x.EmailConfirmed, y => y.Random.Bool())
                .Generate(10);
                
        return users;
    }
    private char GenerateGender()
    {
        Random random = new Random();
        if(random.Next() % 2 == 0)
        {
            return 'M';
        }
        else
        {
            return 'F';
        }
    }
    private byte[] GeneretePassword1()
    {
        string text = "PasswordToHash";
        byte[] byteArray = Encoding.UTF8.GetBytes(text);
        return byteArray;
    }
    private byte[] GeneretePassword2()
    {
        string text = "PasswordToSalt";
        byte[] byteArray = Encoding.UTF8.GetBytes(text);
        return byteArray;
    }
    private IEnumerable<Address> GetAddresses()
    {
        var addresses = new Faker<Address>()
            .RuleFor(x => x.StreetNumber, y => y.Random.Int(1, 255))
            .RuleFor(x => x.ApartmentNumber, y => y.Random.Int(1, 100))
            .RuleFor(x => x.PostalCode, y => y.Address.ZipCode())
            .RuleFor(x => x.Street, y => y.Address.StreetName())
            .RuleFor(x => x.City, y => y.Address.City())
            //.RuleFor(x => x.UserId, y => Guid.NewGuid())
            .Generate(10);
        return addresses;
    }
    private IEnumerable<Product> GetProducts()
    {
        var products = new Faker<Product>()
            .RuleFor(x => x.Name, y => y.Commerce.ProductName())
            .RuleFor(x => x.Supplier, y => y.Company.CompanyName())
            .RuleFor(x => x.Quantity, y => y.Random.Int(0, 1000))
            .RuleFor(x => x.Price, y => y.Random.Decimal(1, 200))
            .Generate(10);
        return products;
    }
    private IEnumerable<Cart> GetCarts()
    {
        var carts = new Faker<Cart>()
            //.RuleFor(x => x.UserId, y => Guid.NewGuid())
            .Generate(10);
        return carts;
    }
    private IEnumerable<CartItem> GetCartItems()
    {
        var cartItems = new Faker<CartItem>()
            .RuleFor(x => x.CartId, y => Guid.NewGuid())
            //.RuleFor(x => x.ProductId, y => Guid.NewGuid())
            //.RuleFor(x => x.Quantity, y => y.Random.Int(1, 80))
            .Generate(10);
        return cartItems;
    }
    private IEnumerable<Rating> GetRatings() 
    {
        var ratings = new Faker<Rating>()
            //.RuleFor(x => x.UserId, y => Guid.NewGuid())
            //.RuleFor(x => x.ProductId, y => Guid.NewGuid())
            .RuleFor(x => x.AddedDate, y => y.Date.Between(new DateTime(2020, 1, 1), new DateTime(2023, 12, 31)))
            .RuleFor(x => x.Rate, y => y.PickRandom<Ratings>())
            .RuleFor(x => x.Comment, y => y.Lorem.Text())
            .Generate(10);
        return ratings;
            
    }
    private IEnumerable<ProductCategory> GetProductCategories() 
    {
        var productCategories = new Faker<ProductCategory>()
            .RuleFor(x => x.CategoryName, y => y.Commerce.Categories(1)[0])
            .Generate(10);
        return productCategories;
    }
    private IEnumerable<Role> GetRoles() 
    {
        string[] roleToGet = { "Admin", "User", "Guest", "Seller" };
        var roles = new Faker<Role>()
            .RuleFor(x => x.Name, y => y.Random.ArrayElement(roleToGet))
            .Generate(10);
        return roles;
    }


}
