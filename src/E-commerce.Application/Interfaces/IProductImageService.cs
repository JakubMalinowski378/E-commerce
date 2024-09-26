using E_commerce.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Application.Interfaces;

public interface IProductImageService
{
    Task HandleImageUploads(Product product, List<IFormFile> images);
}
