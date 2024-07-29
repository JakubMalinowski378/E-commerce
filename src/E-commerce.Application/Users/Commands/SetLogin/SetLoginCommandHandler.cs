using E_commerce.Application.Interfaces;
using E_commerce.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetLogin
{
    internal class SetLoginCommandHandler(IUserRepository userRepository,IUserContext userContext) : IRequestHandler<SetLoginCommnand>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserContext _userContext = userContext;


        public async Task Handle(SetLoginCommnand request, CancellationToken cancellationToken)
        {
           var userFromContext = _userContext.GetCurrentUser();
            var user =await _userRepository.GetUserByIdAsync(userFromContext!.Id);
            user!.Login = request.Login;
            await  _userRepository.SaveChanges();
        }
    }
}
