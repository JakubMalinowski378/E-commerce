using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace E_commerce.Application.Products.Commands.CreateProductCommand;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
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
}
