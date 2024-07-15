using E_commerce.Application.Ratings.Commands.CreateRating;
using E_commerce.Application.Ratings.Commands.DeleteRating;
using E_commerce.Application.Ratings.Commands.UpdateRating;
using E_commerce.Application.Ratings.Dtos;
using E_commerce.Application.Ratings.Queries.GetRating;
using E_commerce.Application.Ratings.Queries.GetRatings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;
[Authorize]
public class RatingController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;
    [HttpPost]
    public async Task<ActionResult> CreateRating(CreateRatingCommand createRatingCommand)
    {
        await _sender.Send(createRatingCommand);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RatingDto>> GetRatingById(Guid id)
    {
        var rating = await _sender.Send(new GetRatingByIdQuery(id));
        return Ok(rating);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatings()
    {
        var ratings = await _sender.Send(new GetRatingsQuary());
        return Ok(ratings);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteRating(Guid id)
    {
        await _sender.Send(new DeleteRatingCommand(id));
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateRating(UpdateRatingCommand updateRatingCommand)
    {
        await _sender.Send(updateRatingCommand);
        return NoContent();
    }
}
