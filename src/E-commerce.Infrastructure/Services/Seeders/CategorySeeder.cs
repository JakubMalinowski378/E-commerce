using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Infrastructure.Services.Seeders;

internal class CategorySeeder(
    ECommerceDbContext dbContext)
    : ISeeder<Category>
{
    public async Task SeedAsync()
    {
        if (await dbContext.Categories.AnyAsync())
            return;

        List<string> categories = [
            "Electronics",
            "Clothing",
            "Home & Kitchen",
            "Beauty & Personal Care",
            "Sports & Outdoors",
            "Automotive",
            "Books",
            "Toys & Games",
            "Health & Household",
            "Grocery & Gourmet Food",
            "Office Products",
            "Pet Supplies",
            "Baby",
            "Tools & Home Improvement",
            "Garden & Outdoor",
            "Shoes",
            "Computers",
            "Video Games",
            "Musical Instruments",
            "Industrial",
            "Sports",
            "Jewelery",
            "Games",
            "Grocery",
            "Kids",
            "Music",
            "Outdoors",
            "Toys",
            "Tools"
            ];

        var categoryEntities = categories.Select(c => new Category
        {
            CategoryName = c
        }).ToList();

        await dbContext.Categories.AddRangeAsync(categoryEntities);
        await dbContext.SaveChangesAsync();
    }
}
