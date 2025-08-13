using E_commerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Infrastructure.Services;
public class EmailNotificationService(
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor
    ) : IEmailNotificationService
{
    private readonly string _appUrl = $"{httpContextAccessor!.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

    public async Task SendConfirmationEmailAsync(string email, string confirmationToken)
    {
        string subject = "Confirm your email";
        string confirmationUrl = $"{_appUrl}/api/Mail/confirm-email?token={confirmationToken}&email={email}";
        string message = $@"
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href='{confirmationUrl}' target='_blank'>Confirm Email</a></p>
            <p>If you did not create this account, you can ignore this message.</p>";
        await emailSender.SendEmailAsync(email, subject, message);
    }

    public async Task SendResetPasswordEmailAsync(string email, string resetPasswordToken)
    {
        string subject = "Reset Password";
        string message = $"Your reset password code is {resetPasswordToken}";

        await emailSender.SendEmailAsync(email, subject, message);
    }
}
