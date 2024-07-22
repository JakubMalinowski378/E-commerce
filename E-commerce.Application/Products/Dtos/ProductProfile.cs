﻿using AutoMapper;
using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Commands.UpdateProductCommand;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Products.Dtos;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(p => p.ProductCategories,
            opt => opt.MapFrom(src => src.Categories.Select(x => x.CategoryName)));

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(p => p.ProductCategories,
            opt => opt.MapFrom(src => src.Categories.Select(x => x.CategoryName)));

        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
