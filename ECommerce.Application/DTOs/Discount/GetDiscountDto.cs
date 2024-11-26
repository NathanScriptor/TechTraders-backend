using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs.Discount
{
    public class GetDiscountDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double DiscountPercent { get; set; }
        public bool IsActive { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
