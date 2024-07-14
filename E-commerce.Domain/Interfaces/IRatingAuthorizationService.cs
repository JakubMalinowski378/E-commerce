using E_commerce.Domain.Entities;

namespace E_commerce.Domain.Interfaces;
public interface IRatingAuthorizationService
{
    bool Authorize(Rating rating);
}
