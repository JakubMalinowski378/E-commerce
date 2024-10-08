﻿using E_commerce.Application.Interfaces;
using E_commerce.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace E_commerce.Infrastructure.Services;

public class EmailSender(IOptions<SmtpSettings> smtpSettings) : IEmailSender
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
        mimeMessage.To.Add(new MailboxAddress(email, email));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart("html") { Text = message };

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, MailKit.Security.SecureSocketOptions.Auto);
        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }
}
