using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure.Data
{
    public class ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserAddress> UsersAddress { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<InventoryVariation> InventoryVariations { get; set; }
        private static string? GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ECommerce.API")).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            return configuration["ConnectionStrings:DefaultConnection"];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(GetConnectionString());
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductDiscount>(entity =>
            {
                entity.HasOne(x => x.Discount)
                    .WithMany(g => g.ProductDiscounts)
                    .HasForeignKey(a => a.DiscountId);
                entity.HasOne(x => x.Product)
                    .WithMany(g => g.ProductDiscounts)
                    .HasForeignKey(a => a.ProductId);

            });

            modelBuilder.Entity<InventoryVariation>(entity => {
                entity.HasOne(x => x.ProductInventory)
                    .WithMany(g => g.InventoryVariations)
                    .HasForeignKey(a => a.ProductInventoryId);
                entity.HasOne(x => x.ProductVariation)
                    .WithMany(g => g.InventoryVariations)
                    .HasForeignKey(a => a.ProductVariationId);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {

                entity.HasOne(oi => oi.ProductInventory)
                    .WithMany(iv => iv.OrderItems)
                    .HasForeignKey(oi => oi.ProductInventoryId)
                    .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete
            });

            // Configure CartItem relationships with Restrict delete behavior
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(ci => ci.ProductInventory)
                    .WithMany(iv => iv.CartItems)
                    .HasForeignKey(ci => ci.ProductInventoryId)
                    .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete

                entity.HasOne(ci => ci.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Restrict);  // Prevents cascade delete
            });
        }
    }
}
