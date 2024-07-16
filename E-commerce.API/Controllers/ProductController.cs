using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Commands.DeleteProductCommand;
using E_commerce.Application.Products.Commands.UpdateProductCommand;
using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
public class ProductController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [AllowAnonymous]
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid productId)
    {
        var product = await _sender.Send(new GetProductByIdQuery(productId));
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(CreateProductCommand command)
    {
        var productId = await _sender.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { productId }, null);
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        await _sender.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    [HttpPatch("{productId}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] Guid productId, UpdateProductCommand command)
    {
        command.ProductId = productId;
        await _sender.Send(command);
        return NoContent();
    }
}
