using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.UserAddress
{
    public class GetUserAddressDto
    {
        public string AddressLine { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country {  get; set; } = string.Empty ;
        public bool DefaultAddress { get; set; }
    }
}
