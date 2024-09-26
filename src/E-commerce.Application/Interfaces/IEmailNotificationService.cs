namespace E_commerce.Application.Interfaces;

public interface IEmailNotificationService
{
    Task SendConfirmationEmailAsync(string email, string confirmationToken);
    Task SendResetPasswordEmailAsync(string email, string resetPasswordToken);
}
