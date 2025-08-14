using E_commerce.Domain.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Application.Features.Products.Commands.CreateProductCommand;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];

    public CreateProductCommandValidator(ICategoryRepository categoryRepository)
    {
        var availableCategories = categoryRepository.GetAllAsync().GetAwaiter().GetResult();

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleForEach(x => x.Images)
            .Must(BeValidExtension)
            .WithMessage($"Valid extensions {string.Join(", ", _allowedExtensions)}");

        RuleForEach(x => x.ProductCategoriesIds)
            .Must(id => availableCategories.Any(c => c.Id == id))
            .WithMessage("Category doesn't exists");
    }

    private bool BeValidExtension(IFormFile file)
    {
        if (file == null)
            return false;
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return _allowedExtensions.Contains(fileExtension);
    }
}
