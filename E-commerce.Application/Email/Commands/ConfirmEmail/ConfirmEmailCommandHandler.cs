using E_commerce.Domain.Repositories;
using MediatR;

namespace E_commerce.Application.Email.Commands.ConfirmEmail;
public class ConfirmEmailCommandHandler(IUserRepository userRepository) : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByConfirmationTokenAsync(request.Token);
        if (user == null || user.ConfirmationTokenExpiration < DateTime.UtcNow) throw new Exception("Invalid confirmation Token");
        user.EmailConfirmed = true;
        user.ConfirmationTokenExpiration = null;
        user.ConfirmationToken = null;
        await _userRepository.SaveUserAsync();
    }
}
