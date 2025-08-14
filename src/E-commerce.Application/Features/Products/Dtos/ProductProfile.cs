using AutoMapper;
using E_commerce.Application.Features.Products.Commands.CreateProductCommand;
using E_commerce.Application.Features.Products.Commands.UpdateProductCommand;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Features.Products.Dtos;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        //TODO: get this value from appsettings.json
        var blobStorageUrl = "http://127.0.0.1:10000/devstoreaccount1/test/";
        CreateMap<Product, ProductDto>()
            .ForMember(p => p.ImageUrls,
                opt => opt.MapFrom(src => src.ProductImagesUrls));

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(p => p.ImageUrls,
                opt => opt.MapFrom(src => src.ProductImagesUrls.Select(x => blobStorageUrl + x)));

        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}
