using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.RegisterByEmail
{
    public class RegisterByEmailCommandValidator:AbstractValidator<RegisterByEmailCommand>
    {
    
        public RegisterByEmailCommandValidator()
        {
            RuleFor(x => x.Password)
                 .NotEmpty()
                 .MinimumLength(8)
                 .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                 .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                 .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                 .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
                
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }

    }
}
