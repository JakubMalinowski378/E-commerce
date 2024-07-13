using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class ProductController(IMediator mediator) : BaseController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid productId)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }
}
