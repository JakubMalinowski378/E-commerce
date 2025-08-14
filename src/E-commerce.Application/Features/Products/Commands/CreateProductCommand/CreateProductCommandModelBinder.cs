using E_commerce.Domain.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_commerce.Application.Features.Products.Commands.CreateProductCommand;
public class CreateProductCommandModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.HttpContext.Request.HasFormContentType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var form = bindingContext.HttpContext.Request.Form;

        var model = new CreateProductCommand()
        {
            Images = [.. form.Files]
        };

        foreach (var key in form.Keys)
        {
            switch (key)
            {
                case nameof(CreateProductCommand.Name):
                    model.Name = form[nameof(CreateProductCommand.Name)]!.ToString();
                    break;
                case nameof(CreateProductCommand.ProductCategoriesIds):
                    model.ProductCategoriesIds = form[nameof(CreateProductCommand.ProductCategoriesIds)]
                        .Select(int.Parse!)
                        .ToList();
                    break;
                case nameof(CreateProductCommand.IsHidden):
                    model.IsHidden = bool.Parse(form[nameof(CreateProductCommand.IsHidden)]!);
                    break;
                case nameof(CreateProductCommand.Quantity):
                    model.Quantity = int.Parse(form[nameof(CreateProductCommand.Quantity)]!);
                    break;
                case nameof(CreateProductCommand.Price):
                    model.Price = decimal.Parse(form[nameof(CreateProductCommand.Price)]!);
                    break;
                default:
                    model.AdditionalProperties[key] = DynamicTypeConverter.Convert(form[key]);
                    break;
            }
        }

        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}
