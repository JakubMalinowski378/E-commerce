namespace E_commerce.Application.E_commerceDtos
{
    public class RegisterDto
    {
        public string Firstname { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public char Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Pasword { get; set; }
    }
}
