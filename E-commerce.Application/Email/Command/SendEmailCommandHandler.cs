using E_commerce.Application.Interfaces;
using MediatR;

namespace E_commerce.Application.Email.Command;
public class SendEmailCommandHandler(IEmailSender emailSender) : IRequestHandler<SendEmailCommand>
{
    private readonly IEmailSender _emailSender = emailSender;

    public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailSender.SendEmailAsync(request.Email, request.Subject, request.Message);
    }
}
