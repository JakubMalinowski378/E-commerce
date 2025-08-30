using FluentValidation;

namespace E_commerce.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
         .NotEmpty().WithMessage("Old password is required.");

        RuleFor(x => x.NewPassword)
         .NotEmpty()
         .MinimumLength(8)
         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
         .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
         .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
         .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}
