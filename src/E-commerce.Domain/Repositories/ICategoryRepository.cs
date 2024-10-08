﻿using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Repositories;
public interface ICategoryRepository
{
    Task Create(Category category);
    Task Delete(Category category);
    Task<IEnumerable<Category>> GetAllAsync();
}
