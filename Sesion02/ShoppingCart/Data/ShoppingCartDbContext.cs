using Microsoft.EntityFrameworkCore;
using ShoppingCartApi.Models;

namespace ShoppingCartApi.Data;

public class ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasData(new List<Product>
            {
                new Product { Id = 1, Name = "Manzana", Price = 10M, Stock = 100 },
                new Product { Id = 2, Name = "Platano", Price = 5M, Stock = 150 },
                new Product { Id = 3, Name = "Cereza", Price = 15M, Stock = 75 },
            });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<decimal>().HavePrecision(13, 2);
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}