using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechCommerce.Models;

namespace TechCommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
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

                var admin = new IdentityRole("Admin");
                admin.NormalizedName = "admin";
                var client = new IdentityRole("Client");
                client.NormalizedName = "client";

                modelBuilder.Entity<IdentityRole>().HasData(admin,client);


            }
        public DbSet<Product> Products { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Cart> Carts { set; get; }

        public DbSet<CartProducts> CartProducts { set; get; }

        public DbSet<OrderProduct> OrderProduct { set; get; }
        public DbSet<Address> Addresses { set; get; }
        public DbSet<ProductRate> Rates { set; get; }


    }
}
