using Microsoft.EntityFrameworkCore;
using TwoAPI.Models;

namespace TwoAPI.DB
{
    public class TwoAPIDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlite("Data Source=twoapi.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubCategory>()
                .HasOne<Category>(x => x.Category)
                .WithMany(y => y.SubCategories)
                .HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Item>()
                .HasOne<SubCategory>(x => x.SubCategory)
                .WithMany(y => y.Items)
                .HasForeignKey(x => x.SubCategoryId);
        }

    }
}
