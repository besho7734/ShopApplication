using Microsoft.EntityFrameworkCore;
using ShopApplication.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopApplication.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Blog> blogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Name = "Clothing", Id = 1 },
                new Category { Name = "Shoes", Id = 2 },
                new Category { Name = "Bags", Id = 3 },
                new Category { Name = "Accessories", Id = 4 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
