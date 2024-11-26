namespace ECommerce.Domain.Entities
{
    public class UserToken : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
