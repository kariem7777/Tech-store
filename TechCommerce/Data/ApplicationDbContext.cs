using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechCommerce.Models;

namespace TechCommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartProducts>().HasKey(p => new { p.ProductId ,p.CartId});
            modelBuilder.Entity<OrderProduct>().HasKey(O => new { O.ProductId, O.OrderId });

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Admin> Admins { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Category> Categories { set; get; }

        public DbSet<Cart> Carts { set; get; }




    }
}
