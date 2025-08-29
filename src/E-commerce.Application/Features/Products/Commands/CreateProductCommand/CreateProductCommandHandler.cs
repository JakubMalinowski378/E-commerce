using AutoMapper;
using E_commerce.Application.Interfaces;
using E_commerce.Domain.Constants;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductCommandHandler(
    IMapper mapper,
    IUserContext userContext,
    IRepository<Product> productRepository,
    IAuthorizationService authorizationService,
    IProductImageService productImageService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);

        if (!await authorizationService.HasPermission(product, ResourceOperation.Create))
            throw new ForbidException();

        var user = userContext.GetCurrentUser();
        product.UserId = user!.Id;

        await productImageService.HandleImageUploads(product, request.Images);
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
