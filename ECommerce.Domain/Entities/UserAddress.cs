using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public string AddressLine { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Country { get; set; } = String.Empty;
        public bool DefaultAddress { get; set; } = false;
        public string UserId { get; set; } = String.Empty;
        public User? User { get; set; }
    }
}
