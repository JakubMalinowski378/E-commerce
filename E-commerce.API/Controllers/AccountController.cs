using E_commerce.Application.E_commerceDtos;
using E_commerce.Application.Intrfaces;
using E_commerce.Domain.Entities;
using E_commerce.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace E_commerce.API.Controllers
{
    public class AccountController :BaseController
    {
        private readonly EcommerceDbContext _context;
        public readonly ITokenService _tokenService;
        public AccountController(EcommerceDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>>Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Email Exist");
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Firstname = registerDto.Firstname,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Gender = registerDto.Gender,
                PhoneNumber = registerDto.PhoneNumber,
                PaswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Pasword)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        private async Task<bool> UserExists(string Email)
        {
            return await _context.Users.AnyAsync(x => x.Email == Email);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.Email==loginDto.Email);
            if (user == null) return NotFound("Invalid Email");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i = 0;i< computedHash.Length;i++)
            {
                if (computedHash[i] != user.PaswordHash[i]) return NotFound("Invalid password");
            }
            return new UserDto
            {
                Email = user.Email,
                Firstname = user.Firstname,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Addresses = user.Addresses,
                EmailConfirmed = user.EmailConfirmed,
                Gender = user.Gender,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
