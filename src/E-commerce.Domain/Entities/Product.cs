﻿namespace E_commerce.Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CartItem> CartItems { get; set; } = default!;
    public List<Category> Categories { get; set; } = default!;
    public List<Rating> Ratings { get; set; } = default!;
    public List<ProductImage> ProductImages { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsHidden { get; set; }
    public User User { get; set; } = default!;
    public Guid UserId { get; set; }
    public string AdditionalProperties { get; set; } = string.Empty;
}
