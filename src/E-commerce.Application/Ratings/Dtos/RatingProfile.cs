using AutoMapper;
using E_commerce.Application.Ratings.Commands.CreateRating;
using E_commerce.Application.Ratings.Commands.UpdateRating;
using E_commerce.Domain.Entities;

namespace E_commerce.Application.Ratings.Dtos;
public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<CreateRatingCommand, Rating>();
        CreateMap<Rating, RatingDto>();
        CreateMap<UpdateRatingCommand, Rating>();
    }
}
