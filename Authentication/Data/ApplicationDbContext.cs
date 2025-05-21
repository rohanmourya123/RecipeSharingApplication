
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Ratings)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ratings)
                .WithOne(rg => rg.Recipe)
                .HasForeignKey(rg => rg.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure composite key for uniqueness in Ratings
            modelBuilder.Entity<Rating>()
                .HasIndex(r => new { r.RecipeId, r.UserId })
                .IsUnique();

            // Additional configurations can be added here as needed
        }

    }
}
