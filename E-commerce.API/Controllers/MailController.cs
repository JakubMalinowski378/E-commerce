using E_commerce.Application.Email.Command;
using E_commerce.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers;

public class MailController(IEmailSender emailSender, ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [Route("forgot-password")]
    public async Task<IActionResult> SendEmail()
    {

        await _sender.Send(new SendEmailCommand("kobrys87@gmail.com", "chuj", "dupa"));
        return Ok();
    }
}
