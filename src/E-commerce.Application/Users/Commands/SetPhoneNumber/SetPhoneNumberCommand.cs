using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetPhoneNumber
{
    public class SetPhoneNumberCommand : IRequest
    {
        public string PhoneNumber { get; set; }
    }
}
