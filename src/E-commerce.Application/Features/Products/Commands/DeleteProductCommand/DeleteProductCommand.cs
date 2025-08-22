using MediatR;

namespace E_commerce.Application.Features.Products.Commands.DeleteProductCommand;

public class DeleteProductCommand(Guid productId) : IRequest
{
    public Guid ProductId { get; set; } = productId;
}
