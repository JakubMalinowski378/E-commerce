using E_commerce.Application.Common;
using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Commands.DeleteProductCommand;
using E_commerce.Application.Products.Commands.UpdateProductCommand;
using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetAllProducts;
using E_commerce.Application.Products.Queries.GetProductByIdQuery;
using E_commerce.Application.Products.Queries.GetUserProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class ProductsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [AllowAnonymous]
    [HttpGet("Products/{productId}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProduct([FromRoute] Guid productId)
    {
        var product = await _sender.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpPost("Products")]
    public async Task<ActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var productId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { productId }, null);
    }

    [HttpDelete("Products/{productId}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        await _sender.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    [HttpPatch("Products/{productId}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] Guid productId, UpdateProductCommand command)
    {
        command.ProductId = productId;
        await _sender.Send(command);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("Products/")]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetProducts([FromQuery] GetAllProductsQuery query)
    {
        var products = await _sender.Send(query);
        return Ok(products);
    }

    [HttpGet("Users/{userId}/Products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetUserProducts([FromRoute] Guid userId)
    {
        var products = await _sender.Send(new GetUserProducts(userId));
        return Ok(products);
    }
}
