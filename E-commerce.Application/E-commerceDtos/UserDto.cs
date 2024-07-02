using E_commerce.Domain.Entities;

namespace E_commerce.Application.E_commerceDtos
{
    public class UserDto
    {
        public string Firstname { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public char Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public List<Address> Addresses { get; set; } = default!;
    }
}
