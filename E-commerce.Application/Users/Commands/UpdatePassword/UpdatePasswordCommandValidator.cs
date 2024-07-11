﻿using FluentValidation;

namespace E_commerce.Application.Users.Commands.UpdatePassword;
public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
         .NotEmpty()
         .MinimumLength(8)
         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
         .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
         .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
         .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
