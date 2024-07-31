using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetLogin
{
    public class SetLoginCommand : IRequest
    {
        public string Login { get; set; } = string.Empty;
    }
}
