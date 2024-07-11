using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest
    {
        public string Email { get; set; }    
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
