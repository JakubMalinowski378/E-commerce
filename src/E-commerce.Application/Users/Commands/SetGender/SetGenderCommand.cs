using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetGender
{
    public class SetGenderCommand : IRequest
    {
        public char Gender { get; set; }
    }
}
