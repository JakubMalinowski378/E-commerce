using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Users.Commands.SetGender
{
    public class SetGenderCommandValidator : AbstractValidator<SetGenderCommand>
    {
        public SetGenderCommandValidator()
        {
            RuleFor(x => x.Gender)
                .NotEmpty()
                .Must(x => x == 'M' || x == 'F');

        }
    }
}
