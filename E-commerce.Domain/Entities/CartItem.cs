﻿namespace E_commerce.Domain.Entities;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;
}