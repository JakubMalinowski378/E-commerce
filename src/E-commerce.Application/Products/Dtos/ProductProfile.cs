using AutoMapper;
using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Commands.UpdateProductCommand;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Products.Dtos;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        //TODO: get this value from appsettings.json
        var blobStorageUrl = "http://127.0.0.1:10000/devstoreaccount1/test/";
        CreateMap<Product, ProductDto>()
            .ForMember(p => p.ProductCategories,
                opt => opt.MapFrom(src => src.Categories.Select(x => x.CategoryName)))
            .ForMember(p => p.ImageUrls,
                opt => opt.MapFrom(src => src.ProductImages.Select(x => blobStorageUrl + x.FileName)));

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(p => p.ProductCategories,
                opt => opt.MapFrom(src => src.Categories.Select(x => x.CategoryName)))
            .ForMember(p => p.ImageUrls,
                opt => opt.MapFrom(src => src.ProductImages.Select(x => blobStorageUrl + x.FileName)));

        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
