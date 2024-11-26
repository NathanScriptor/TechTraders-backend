namespace ECommerce.Application.DTOs.Auth
{
    public class SignUpDto
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Phonenumber { get; set; }
        public required string ConfirmedPassword { get; set; }
    }
}
