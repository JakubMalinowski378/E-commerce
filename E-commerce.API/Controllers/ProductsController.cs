using E_commerce.Application.Products.Commands.CreateProductCommand;
using E_commerce.Application.Products.Commands.DeleteProductCommand;
using E_commerce.Application.Products.Commands.UpdateProductCommand;
using E_commerce.Application.Products.Dtos;
using E_commerce.Application.Products.Queries.GetProductByIdQuery;
using E_commerce.Application.Ratings.Commands.CreateRating;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Application.Ratings.Queries.GetProductRatings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;
[Authorize]
public class ProductsController(ISender sender) : BaseController
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

    [HttpPost("{productId}/ratings")]
    public async Task<ActionResult> CreateRating([FromRoute] Guid productId, CreateRatingCommand command)
    {
        command.ProductId = productId;
        await _sender.Send(command);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("{productId}/ratings")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetProductRatings([FromRoute] Guid productId)
    {
        var ratings = await _sender.Send(new GetProductRatingsQuery(productId));
        return Ok(ratings);
    }
}
