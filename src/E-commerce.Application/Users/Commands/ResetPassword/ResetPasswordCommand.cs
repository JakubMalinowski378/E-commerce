﻿using MediatR;
using System.Text.Json.Serialization;

namespace E_commerce.Application.Users.Commands.ResetPassword;
public class ResetPasswordCommand : IRequest
{
    [JsonIgnore]
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
