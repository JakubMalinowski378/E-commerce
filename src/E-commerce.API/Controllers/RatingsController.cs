using E_commerce.Application.Ratings.Commands.DeleteRating;
using E_commerce.Application.Ratings.Commands.UpdateRating;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Application.Ratings.Queries.GetRating;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;
[Authorize]
public class RatingsController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpGet("{id}")]
    public async Task<ActionResult<RatingDto>> GetRatingById(Guid id)
    {
        var rating = await _sender.Send(new GetRatingByIdQuery(id));
        return Ok(rating);
    }

    [HttpDelete("{ratingId}")]
    public async Task<ActionResult> DeleteRating([FromRoute] Guid ratingId)
    {
        await _sender.Send(new DeleteRatingCommand(ratingId));
        return NoContent();
    }

    [HttpPatch("{ratingId}")]
    public async Task<ActionResult> UpdateRating([FromRoute] Guid ratingId, UpdateRatingCommand command)
    {
        command.RatingId = ratingId;
        await _sender.Send(command);
        return NoContent();
    }
}
