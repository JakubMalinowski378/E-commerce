using E_commerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Infrastructure.Services;
public class EmailNotificationService : IEmailNotificationService
{
    private readonly IEmailSender _emailNotificationService;
    private readonly string _appUrl;

    public EmailNotificationService(IEmailSender emailSender)
    {
        _emailNotificationService = emailSender;
        var contextAccessor = new HttpContextAccessor();
        _appUrl = $"{contextAccessor!.HttpContext!.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}";
    }

    public async Task SendConfirmationEmailAsync(string email, string confirmationToken)
    {
        string subject = "Confirm your email";
        string confirmationUrl = $"{_appUrl}/api/Mail/confirm-email?token={confirmationToken}&email={email}";
        string message = $@"
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href='{confirmationUrl}' target='_blank'>Confirm Email</a></p>
            <p>If you did not create this account, you can ignore this message.</p>";
        await _emailNotificationService.SendEmailAsync(email, subject, message);
    }

    public async Task SendResetPasswordEmailAsync(string email, string resetPasswordToken)
    {
        string subject = "Reset Password";
        string message = $"Your reset password code is {resetPasswordToken}";

        await _emailNotificationService.SendEmailAsync(email, subject, message);
    }
}
