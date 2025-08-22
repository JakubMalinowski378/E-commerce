using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Features.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IAuthorizationService authorizationService,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId)
            ?? throw new NotFoundException(nameof(Product), request.ProductId.ToString());

        if (!await authorizationService.HasPermission(product, "Update"))
        {
            throw new ForbidException();
        }

        mapper.Map(request, product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
