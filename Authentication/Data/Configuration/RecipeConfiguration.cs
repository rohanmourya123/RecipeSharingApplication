//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Data.Entities;

//namespace Data.Configurations
//{
//    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
//    {
//        public void Configure(EntityTypeBuilder<Recipe> builder)
//        {
//            // Define the primary key for Recipe
//            builder.HasKey(r => r.Id);

//            // Configure the Title property
//            builder.Property(r => r.Title)
//                .IsRequired()
//                .HasMaxLength(200); // Title is required and limited to 200 characters

//            // Configure the Category property
//            builder.Property(r => r.Category)
//                .IsRequired()
//                .HasMaxLength(100); // Category is required and limited to 100 characters

//            // Configure the Description property
//            builder.Property(r => r.Description)
//                .HasMaxLength(1000); // Description is optional, with a maximum of 1000 characters

//            // Configure the ImageContentType property
//            builder.Property(r => r.ImageContentType)
//                .HasMaxLength(100); // Image content type is optional, with a maximum of 100 characters

//            // Recipe → User (ApplicationUser) relationship
//            builder.HasOne(r => r.User)
//                .WithMany(u => u.Recipes)
//                .HasForeignKey(r => r.UserId)
//                .OnDelete(DeleteBehavior.SetNull); // If User is deleted, set the UserId to null

//            // Recipe → Ingredients relationship
//            builder.HasMany(r => r.Ingredients)
//                .WithOne(i => i.Recipe)
//                .HasForeignKey(i => i.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // If Recipe is deleted, set RecipeId to null for Ingredients

//            // Recipe → Instructions relationship
//            builder.HasMany(r => r.Instructions)
//                .WithOne(i => i.Recipe)
//                .HasForeignKey(i => i.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // If Recipe is deleted, set RecipeId to null for Instructions

//            // Recipe → Ratings relationship
//            builder.HasMany(r => r.Ratings)
//                .WithOne(rt => rt.Recipe)
//                .HasForeignKey(rt => rt.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // If Recipe is deleted, set RecipeId to null for Ratings

//            // Recipe → Comments relationship
//            builder.HasMany(r => r.Comments)
//                .WithOne(c => c.Recipe)
//                .HasForeignKey(c => c.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // If Recipe is deleted, set RecipeId to null for Comments

//            // Make sure the foreign key properties are nullable
//            builder.Property(r => r.UserId)
//                .IsRequired(false); // UserId is optional (nullable)

//            // Optional: Ensure that the RecipeId foreign keys in other related entities are nullable
//            builder.Property(r => r.Id)
//                .IsRequired(); // RecipeId is required (primary key)
//        }
//    }
//}
