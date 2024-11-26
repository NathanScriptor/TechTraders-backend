namespace ECommerce.Domain.Entities
{
    public class ProductDetail : BaseEntity
    {
        public string ProductId { get; set; } = string.Empty;
        public Product? Product { get; set; } 
        public string Brand { get; set; } = string.Empty; // Example: "Apple", "Samsung"
        public string OperatingSystem { get; set; } = string.Empty; // Example: "iOS", "Android"
        public double ScreenSize { get; set; } // Example: 6.1 inches
        public int BatteryCapacity { get; set; } // Example: 4000 mAh
        public int StorageCapacity { get; set; } // Example: 128GB, 256GB
        public int RAM { get; set; } // Example: 6GB
        public string Color { get; set; } = string.Empty;  // Example: "Black", "White"
        public DateTime? DeletedAt { get; set; }
    }
}
