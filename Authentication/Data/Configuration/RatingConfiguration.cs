//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Data.Entities;

//namespace Data.Configurations
//{
//    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
//    {
//        public void Configure(EntityTypeBuilder<Rating> builder)
//        {
//            // Define the primary key for Rating
//            builder.HasKey(r => r.RatingId);

//            // Configure the RatingValue property
//            builder.Property(r => r.RatingValue)
//                .IsRequired(); // RatingValue must be required

//            // Configure CreatedAt with default value
//            builder.Property(r => r.CreatedAt)
//                .HasDefaultValueSql("GETDATE()"); // Default value set to current date

//            // Rating → User (ApplicationUser) relationship
//            builder.HasOne(r => r.User)
//                .WithMany(u => u.Ratings)
//                .HasForeignKey(r => r.UserId)
//                .OnDelete(DeleteBehavior.SetNull); // Set UserId to null if User is deleted

//            // Rating → Recipe relationship
//            builder.HasOne(r => r.Recipe)
//                .WithMany(recipe => recipe.Ratings)
//                .HasForeignKey(r => r.RecipeId)
//                .OnDelete(DeleteBehavior.SetNull); // Set RecipeId to null if Recipe is deleted

//            // Make sure the foreign keys are nullable
//            builder.Property(r => r.UserId)
//                .IsRequired(false); // UserId is nullable

//            builder.Property(r => r.RecipeId)
//                .IsRequired(false); // RecipeId is nullable
//        }
//    }
//}
