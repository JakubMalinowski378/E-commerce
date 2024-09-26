using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.Application.Users.Commands.UpdatePassword;
public class UpdatePasswordCommandHandler(IUserRepository userRepository, IUserContext userContext)
    : IRequestHandler<UpdatePasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserContext _userContext = userContext;

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var userContext = _userContext.GetCurrentUser();

        var user = await _userRepository.GetUserByEmailAsync(userContext!.Email)
            ?? throw new NotFoundException(nameof(User), userContext.Email);

        var hmac = new HMACSHA512();
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.OldPassword));

        if (!computedHash.SequenceEqual(user.PasswordHash))
            throw new ForbidException("Old password is incorrect");

        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
        user.PasswordSalt = hmac.Key;
        await _userRepository.SaveChanges();
    }
}