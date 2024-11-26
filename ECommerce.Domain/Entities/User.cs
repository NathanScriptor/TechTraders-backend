namespace ECommerce.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
