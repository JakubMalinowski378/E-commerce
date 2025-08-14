using AutoMapper;
using E_commerce.Application.Features.Ratings.Commands.CreateRating;
using E_commerce.Application.Features.Ratings.Commands.UpdateRating;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Features.Ratings.Dtos;
public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<CreateRatingCommand, Rating>();
        CreateMap<Rating, RatingDto>();
        CreateMap<UpdateRatingCommand, Rating>();
    }
}
