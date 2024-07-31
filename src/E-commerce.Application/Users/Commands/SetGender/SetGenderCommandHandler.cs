using E_commerce.Application.Interfaces;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetGender
{
    public class SetGenderCommandHandler(IUserRepository userRepository,IUserContext userContext) : IRequestHandler<SetGenderCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task Handle(SetGenderCommand request, CancellationToken cancellationToken)
        {
            var currenUser = _userContext.GetCurrentUser();
            var user =await _userRepository.GetUserByEmailAsync(currenUser.Email)??await _userRepository.GetUserByPhoneNumberAsync(currenUser.PhoneNumber);
            user!.Gender = request.Gender;
            await _userRepository.SaveChanges();
        }
    }
}
