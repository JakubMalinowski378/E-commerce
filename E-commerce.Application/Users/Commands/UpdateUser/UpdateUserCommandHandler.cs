using AutoMapper;
using E_commerce.Domain.Entities;
using E_commerce.Domain.Exceptions;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        : IRequestHandler<UpdateUserCommand,Guid>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper;
        public async Task<Guid>Handle(UpdateUserCommand request,CancellationToken cancel)
        {
            if (!await _userRepository.UserExists(request.Email))
                throw new NotFoundException(nameof(User), request.Email);
            var user = _mapper.Map<User>(request);
            await _userRepository.UpdateUserAsync(user);
            return user.Id;
        } 
    }
}
