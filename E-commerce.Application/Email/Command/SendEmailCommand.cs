using MediatR;

namespace E_commerce.Application.Email.Command;
public class SendEmailCommand(string email, string subject, string message) : IRequest
{
    public string Email { get; } = email;
    public string Subject { get; } = subject;
    public string Message { get; } = message;
}
