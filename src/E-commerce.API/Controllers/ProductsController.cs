using E_commerce.Application.Common;
using E_commerce.Application.Features.Products.Commands.CreateProductCommand;
using E_commerce.Application.Features.Products.Commands.DeleteProductCommand;
using E_commerce.Application.Features.Products.Commands.UpdateProductCommand;
using E_commerce.Application.Features.Products.Dtos;
using E_commerce.Application.Features.Products.Queries.GetAllProducts;
using E_commerce.Application.Features.Products.Queries.GetProductByIdQuery;
using E_commerce.Application.Features.Products.Queries.GetUserProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class ProductsController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("Products/{productId}")]
    public async Task<ActionResult<ProductDetailsDto>> GetProduct([FromRoute] Guid productId)
    {
        var product = await sender.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpPost("Products")]
    public async Task<ActionResult> CreateProduct([FromForm] CreateProductCommand command)
    {
        var productId = await sender.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { productId }, null);
    }

    [HttpDelete("Products/{productId}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        await sender.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    [HttpPut("Products/{productId}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] Guid productId, [FromForm] UpdateProductCommand command)
    {
        command.ProductId = productId;
        await sender.Send(command);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("Products/")]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetProducts([FromQuery] GetAllProductsQuery query)
    {
        var products = await sender.Send(query);
        return Ok(products);
    }

    [HttpGet("Users/{userId}/Products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetUserProducts([FromRoute] Guid userId)
    {
        var products = await sender.Send(new GetUserProducts(userId));
        return Ok(products);
    }
}
