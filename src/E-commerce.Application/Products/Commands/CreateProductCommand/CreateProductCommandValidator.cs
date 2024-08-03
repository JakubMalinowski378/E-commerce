using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.AdditionalProperties)
            .Must(BeValidJson)
            .WithMessage("Invalid JSON format.");

        RuleForEach(x => x.Images)
            .Must(BeValidExtension)
            .WithMessage($"Valid extensions {string.Join(", ", _allowedExtensions)}");
    }

    private bool BeValidJson(string jsonString)
    {
        try
        {
            JToken.Parse(jsonString);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }

    private bool BeValidExtension(IFormFile file)
    {
        if (file == null)
            return false;


        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        return _allowedExtensions.Contains(fileExtension);
    }
}
