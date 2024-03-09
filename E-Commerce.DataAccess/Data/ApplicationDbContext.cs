using E_Commerce.Models.Models;
using Microsoft.EntityFrameworkCore;


namespace E_Commerce.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
            new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );
            modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "Ketchup", Description = "Heinz Tomato Ketchup", Price = 120, CategoryId=1 ,ImageUrl=""},
            new Product { Id = 2, Title = "Mixed Fruit Juice", Description = "Mixed Fruit Goodness", Price = 110, CategoryId=2, ImageUrl = "" },
            new Product { Id = 3, Title = "Aashirwad Aata", Description = "MultiGrain Aaata", Price = 300, CategoryId = 1, ImageUrl = "" }
            );
        }
    }
    
}
