using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class ProductController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid productId)
    {
        var product = await _sender.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateProduct(CreateProductCommand command)
    {
        var productId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { productId }, null);
    }
}
