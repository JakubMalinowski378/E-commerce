using E_commerce.Application.Interfaces;
using E_commerce.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_commerce.Application.Extensions;

public static class AplicationServiceExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration config)
    {
        var assembly = typeof(AplicationServiceExtension).Assembly;

        services.AddScoped<ITokenService, TokenService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Token key was not found");
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false

            };
        });
        services.AddAuthorization();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();

        services.AddAutoMapper(assembly);

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }
}
