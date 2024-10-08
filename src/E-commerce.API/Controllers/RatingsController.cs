﻿using E_commerce.Application.Ratings.Commands.CreateRating;
using E_commerce.Application.Ratings.Commands.DeleteRating;
using E_commerce.Application.Ratings.Commands.UpdateRating;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Application.Ratings.Queries.GetProductRatings;
using E_commerce.Application.Ratings.Queries.GetRating;
using E_commerce.Application.Users.Queries.GetUserRatingsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class RatingsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("Ratings/{ratingId}")]
    public async Task<ActionResult<RatingDto>> GetRatingById(Guid ratingId)
    {
        var rating = await _sender.Send(new GetRatingByIdQuery(ratingId));
        return Ok(rating);
    }

    [HttpDelete("Ratings/{ratingId}")]
    public async Task<ActionResult> DeleteRating([FromRoute] Guid ratingId)
    {
        await _sender.Send(new DeleteRatingCommand(ratingId));
        return NoContent();
    }

    [HttpPatch("Ratings/{ratingId}")]
    public async Task<ActionResult> UpdateRating([FromRoute] Guid ratingId, UpdateRatingCommand command)
    {
        command.RatingId = ratingId;
        await _sender.Send(command);
        return NoContent();
    }

    [HttpPost("Products/{productId}/Ratings")]
    public async Task<ActionResult> CreateRating([FromRoute] Guid productId, CreateRatingCommand command)
    {
        command.ProductId = productId;
        await _sender.Send(command);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpGet("Products/{productId}/Ratings")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetProductRatings([FromRoute] Guid productId)
    {
        var ratings = await _sender.Send(new GetProductRatingsQuery(productId));
        return Ok(ratings);
    }

    [HttpGet("Users/{userId}/Ratings")]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetAllRatingsOfUser(Guid userId)
    {
        var ratings = await _sender.Send(new GetUserRatingsQuery(userId));
        return Ok(ratings);
    }
}
