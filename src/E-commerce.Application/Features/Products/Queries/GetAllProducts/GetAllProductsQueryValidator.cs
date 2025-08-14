using FluentValidation;

namespace E_commerce.Application.Features.Products.Queries.GetAllProducts;
public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    private readonly int[] _allowedPageSize = [5, 10, 15, 30];
    public GetAllProductsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .Must(x => _allowedPageSize.Contains(x))
            .WithMessage($"Page size must be in [{string.Join(",", _allowedPageSize)}]");
    }
}
