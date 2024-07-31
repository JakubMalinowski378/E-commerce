using E_commerce.Application.Interfaces;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetNameAndLastName
{
    public class SetNameAndLastNameCommandHandler(IUserRepository userRepository,IUserContext userContext) : IRequestHandler<SetNameAndLastNameCommand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task Handle(SetNameAndLastNameCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            var user = await _userRepository.GetUserByEmailAsync(currentUser!.Email);
            user!.Firstname = request.Name;
            user.LastName = request.LastName;
            await _userRepository.SaveChanges();
            
        }
    }
}
