using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommand : IRequest
    {
        public Guid UserId { get; set; }    
        public string Email { get; set; } = string.Empty;
    }
}
