namespace ECommerce.Application.DTOs.User
{
    public class GetUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
