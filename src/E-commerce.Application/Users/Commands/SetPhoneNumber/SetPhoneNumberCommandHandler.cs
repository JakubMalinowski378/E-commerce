using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetPhoneNumber
{
    public class SetPhoneNumberCommandHandler(IUserRepository userRepository,IUserContext userContext) : IRequestHandler<SetPhoneNumberCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task Handle(SetPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userRepository.GetUserByEmailAsync(currentUser.Email);
            var userByPhone = await _userRepository.GetUserByPhoneNumberAsync(request.PhoneNumber);
            if(user is not null)
            {
                if (userByPhone is not null){
                    if (!user.Equals(userByPhone))
                    {
                        throw new Exception("phone number have to be unique");
                    }
                    user = userByPhone;
                }
            }
            
            user!.PhoneNumber = request.PhoneNumber;
            await _userRepository.SaveChanges();
        }
    }
}
