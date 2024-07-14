using E_commerce.Application.Ratings.Commands.CreateRating;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class RatingController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateRating(CreateRatingCommand createRatingCommand)
    {
        await _sender.Send(createRatingCommand);
        return NoContent();
    }
}
