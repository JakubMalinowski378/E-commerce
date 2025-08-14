using E_commerce.Domain.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_commerce.Application.Features.Products.Commands.UpdateProductCommand;
public class UpdateProductCommandModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.HttpContext.Request.HasFormContentType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var form = bindingContext.HttpContext.Request.Form;

        var model = new UpdateProductCommand()
        {
            Images = [.. form.Files]
        };

        foreach (var key in form.Keys)
        {
            switch (key)
            {
                case nameof(UpdateProductCommand.Name):
                    model.Name = form[nameof(UpdateProductCommand.Name)]!.ToString();
                    break;
                case nameof(UpdateProductCommand.ProductCategoriesIds):
                    model.ProductCategoriesIds = form[nameof(UpdateProductCommand.ProductCategoriesIds)]
                        .Select(int.Parse!)
                        .ToList();
                    break;
                case nameof(UpdateProductCommand.IsHidden):
                    model.IsHidden = bool.Parse(form[nameof(UpdateProductCommand.IsHidden)]!);
                    break;
                case nameof(UpdateProductCommand.Quantity):
                    model.Quantity = int.Parse(form[nameof(UpdateProductCommand.Quantity)]!);
                    break;
                case nameof(UpdateProductCommand.Price):
                    model.Price = decimal.Parse(form[nameof(UpdateProductCommand.Price)]!);
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